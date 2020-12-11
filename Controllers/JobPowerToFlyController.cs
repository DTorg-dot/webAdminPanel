using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models;
using WebAdminPanel.Models.Base;
using WebAdminPanel.Models.Enum;
using WebAdminPanel.Models.PowerToFly;

namespace WebAdminPanel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPowerToFlyController : ControllerBase
    {
        public DatabaseContext Context { get; set; }

        public string SiteName { get; set; } = "Powertofly";

        public JobPowerToFlyController(DatabaseContext context)
        {
            Context = context;
        }

        [HttpPost("ParseByLinks")]
        public async Task ParseByJobLinks([FromBody]ParseByJobLinkDto parseByJobLinkDto)
        {
            if (parseByJobLinkDto.IgnoreAlreadySended)
            {
                await RemoveAlreadySentedJob(parseByJobLinkDto);
            }

            var account = await Context.AccountsPowerToFly.FirstAsync(x => x.Email == parseByJobLinkDto.AccountEmail);
            var site = await Context.Sites.FirstAsync(x => x.Name == SiteName);

            var botSignals = new BotSignal
            {
                Account = account,
                BotTypeSignal = BotTypeSignal.SendCoverLetterToJobsByLinks,
                IgnoreAlreadySended = parseByJobLinkDto.IgnoreAlreadySended,
                SiteId = site.Id,
                JobLinks = parseByJobLinkDto.JobLinks.Trim(),
                CoverrLetter = parseByJobLinkDto.CoverLetter,
                Status = BotSignalStatus.Waiting
            };

            Context.BotSignals.Add(botSignals);
            await Context.SaveChangesAsync();
        }

        private async Task RemoveAlreadySentedJob(ParseByJobLinkDto parseByJobLinkDto)
        {
            var splitedLinks = parseByJobLinkDto.JobLinks.Split(";").Select(x => x.Trim()).ToList();

            var existedJobs = await Context.JobsPowerToFly.Where(x => x.Account.Email == parseByJobLinkDto.AccountEmail).Select(x => x.Link).ToListAsync();
            parseByJobLinkDto.JobLinks = string.Join(";", splitedLinks.Where(x => !existedJobs.Contains(x)));
        }

        [HttpGet("BotSignal")]
        public async Task<BotSignalDto> GetBotSignal()
        {
            var site = Context.Sites.First(x => x.Name == SiteName);
            var botSignal = Context.BotSignals.FirstOrDefault(x => x.SiteId == site.Id && x.Status == BotSignalStatus.Waiting);
            var account = Context.AccountsPowerToFly.First(x => x.Id == botSignal.AccountId && x.Status == Models.Enum.AccountStatus.Available);

            account.Status = Models.Enum.AccountStatus.Working;
            botSignal.Status = BotSignalStatus.InProgress;
            await Context.SaveChangesAsync();

            botSignal.Account = account;

            return new BotSignalDto {
                Email = account.Email,
                Password = account.Password,
                CoverLetter = botSignal.CoverrLetter,
                IgnoreAlreadySended = botSignal.IgnoreAlreadySended,
                JobLinks = botSignal.JobLinks
            };
        }

        [HttpGet("ChangeStatus")]
        public async Task ChangeAccountStatus([FromQuery]string email, [FromQuery]string status)
        {
            var account = await Context.AccountsPowerToFly.FirstAsync(x => x.Email == email);
            account.Status = (AccountStatus)Convert.ToInt32(status);
            await Context.SaveChangesAsync();
        }

        [HttpGet("SaveJob")]
        public async Task SaveJob([FromBody]ICollection<JobPowerToFly> jobPowerToFlies)
        {
            Context.JobsPowerToFly.AddRange(jobPowerToFlies);

            await Context.SaveChangesAsync();
        }
    }

    public class ChangeAccountStatus
    {
        public string Email { get; set; }

        public AccountStatus Status { get; set; }
    }

    public class ParseByJobLinkDto
    {
        public string AccountEmail { get; set; }

        public string JobLinks { get; set; }

        public string CoverLetter { get; set; }

        public bool IgnoreAlreadySended { get; set; }
    }

    public class BotSignalDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string JobLinks { get; set; }

        public string CoverLetter { get; set; }

        public bool IgnoreAlreadySended { get; set; }
    }
}
