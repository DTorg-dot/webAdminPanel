using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models;
using WebAdminPanel.Models.Enum;
using WebAdminPanel.Models.FlexJob;

namespace WebAdminPanel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlexJobController
    {
        public DatabaseContext Context { get; set; }
        public string SiteName { get; set; } = "FlexJob";

        public FlexJobController(DatabaseContext context)
        {
            Context = context;
        }

        [HttpGet("Accounts")]
        public async Task<ICollection<AccountFlexJob>> GetFlexJobAccounts()
        {
            List<AccountFlexJob> accounts = await Context.AccountsFlexJob.ToListAsync();

            return accounts;
        }

        [HttpPost("AddAccount")]
        public async Task AddNewAccount([FromBody]AccountFlexJob account)
        {
            AccountFlexJob accountFlexJob = new AccountFlexJob
            {
                Email = account.Email,
                Password = account.Password,
                SiteId = account.SiteId,
                Status = Models.Enum.AccountStatus.Available
            };

            Context.AccountsFlexJob.Add(accountFlexJob);

            await Context.SaveChangesAsync();
        }

        [HttpPost("ParseByLinks")]
        public async Task<IActionResult> ParseByJobLinks([FromBody]ParseByJobLinkDto parseByJobLinkDto)
        {
            if (parseByJobLinkDto.IgnoreAlreadySended)
            {
                await RemoveAlreadySentedJob(parseByJobLinkDto);
            }

            var account = await Context.AccountsFlexJob.FirstAsync(x => x.Email == parseByJobLinkDto.AccountEmail);

            if (account == null)
            {
                return new BadRequestObjectResult(new string("Account not found"));
            }

            var site = await Context.Sites.FirstAsync(x => x.Name == SiteName);

            if (site == null)
            {
                return new BadRequestObjectResult(new string("Site not found"));
            }

            var botSignals = new BotSignalFlexJob
            {
                Account = account,
                BotTypeSignal = BotTypeSignal.SendCoverLetterToJobsByLinks,
                IgnoreAlreadySended = parseByJobLinkDto.IgnoreAlreadySended,
                JobLinks = parseByJobLinkDto.JobLinks.Trim(),
                CoverrLetter = parseByJobLinkDto.CoverLetter,
                Status = BotSignalStatus.Waiting,
                ProfileId = parseByJobLinkDto.ProfileId
            };

            Context.BotSignalFlexJob.Add(botSignals);
            await Context.SaveChangesAsync();

            return new OkObjectResult(new { Message = "Bot signal created" });
        }

        [HttpGet("BotSignal")]
        public async Task<IActionResult> GetBotSignal()
        {
            var site = Context.Sites.First(x => x.Name == SiteName);

            if (site == null)
            {
                return new BadRequestObjectResult(new string("Site not found"));
            }

            var botSignal = Context.BotSignalFlexJob.Include(x => x.Account).FirstOrDefault(x => x.Account.SiteId == site.Id && x.Status == BotSignalStatus.Waiting);
            if (botSignal == null)
            {
                return new BadRequestObjectResult(new string("Bot Signal not found"));
            }

            var account = botSignal.Account;

            account.Status = Models.Enum.AccountStatus.Working;
            botSignal.Status = BotSignalStatus.InProgress;
            await Context.SaveChangesAsync();

            botSignal.Account = account;

            var signal = new BotSignalDto
            {
                Id = botSignal.Id,
                Email = account.Email,
                Password = account.Password,
                CoverLetter = botSignal.CoverrLetter,
                IgnoreAlreadySended = botSignal.IgnoreAlreadySended,
                JobLinks = botSignal.JobLinks
            };

            return new OkObjectResult(signal);
        }

        [HttpGet("Status")]
        public async Task<IActionResult> GetStatus()
        {
            var site = Context.Sites.First(x => x.Name == SiteName);

            if (site == null)
            {
                return new BadRequestObjectResult(new string("Site not found"));
            }

            var botSignal = await Context.BotSignalFlexJob.Include(x => x.Account).FirstOrDefaultAsync(x => x.Account.SiteId == site.Id && x.Status == BotSignalStatus.InProgress);
            if (botSignal == null)
            {
                return new BadRequestObjectResult(new string("Bot Signal not found"));
            }

            var doneJobsCount = await Context.JobsFlexJob.Where(x => x.SignalId == botSignal.Id).CountAsync();

            return new OkObjectResult(new Status
            {
                AllCount = botSignal.JobLinks.Split(';').Where(x => !string.IsNullOrEmpty(x)).Count(),
                DoneCount = doneJobsCount
            });
        }

        [HttpGet("ChangeStatus")]
        public async Task ChangeAccountStatus([FromQuery]string email, [FromQuery]string status)
        {
            var account = await Context.AccountsFlexJob.FirstAsync(x => x.Email == email);
            account.Status = (AccountStatus)Convert.ToInt32(status);
            await Context.SaveChangesAsync();
        }

        [HttpPost("ChangeSignalStatus")]
        public async Task FinishBotSignalStatus([FromQuery]int botSignalId, [FromQuery] BotSignalStatus status)
        {
            var botSignal = await Context.BotSignalFlexJob.FirstAsync(x => x.Id == botSignalId);
            botSignal.Status = status;
            await Context.SaveChangesAsync();
        }

        [HttpPost("SaveJob")]
        public async Task SaveJob([FromBody]JobDto job)
        {
            Context.JobsFlexJob.Add(
                new JobFlexJob
                {
                    Name = job.Name,
                    Link = job.Link,
                    CoverLetter = job.CoverLetter,
                    SignalId = job.SignalId
                });

            await Context.SaveChangesAsync();
        }

        private async Task RemoveAlreadySentedJob(ParseByJobLinkDto parseByJobLinkDto)
        {
            var splitedLinks = parseByJobLinkDto.JobLinks.Split(";").Select(x => x.Trim()).ToList();

            List<string> existedJobs = await Context.BotSignalFlexJob
                .Where(x => x.Account.Email == parseByJobLinkDto.AccountEmail)
                .Select(x => x.JobLinks.Trim())
                .ToListAsync();

            parseByJobLinkDto.JobLinks = string.Join(";", splitedLinks.Where(x => !existedJobs.Contains(x.Trim())));
        }

        public class ParseByJobLinkDto
        {
            public string AccountEmail { get; set; }

            public string JobLinks { get; set; }

            public string CoverLetter { get; set; }

            public bool IgnoreAlreadySended { get; set; }

            public int ProfileId { get; set; }
        }

        public class BotSignalDto
        {
            public int Id { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string JobLinks { get; set; }

            public string CoverLetter { get; set; }

            public bool IgnoreAlreadySended { get; set; }
        }

        public class AccountJobFlexDto
        {
            public string Email { get; set; }

            public string Password { get; set; }

            public int SiteId { get; set; }
        }

        public class JobDto
        {
            public string Name { get; set; }

            public string Link { get; set; }

            public string CoverLetter { get; set; }

            public int SignalId { get; set; }
        }

        public class Status
        {
            public int DoneCount { get; set; }

            public int AllCount { get; set; }
        }
    }
}
