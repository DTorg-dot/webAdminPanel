﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models;
using WebAdminPanel.Models.Enum;
using WebAdminPanel.Models.PowerToFly;

namespace WebAdminPanel.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PowertoflyController
    {
        public DatabaseContext Context { get; set; }
        public string SiteName { get; set; } = "Powertofly";

        public PowertoflyController(DatabaseContext context)
        {
            Context = context;
        }

        [HttpGet("Accounts")]
        public async Task<ICollection<AccountPowerToFly>> GetPowerToFlyAccounts()
        {
            List<AccountPowerToFly> accounts = await Context.AccountsPowerToFly.ToListAsync();

            return accounts;
        }

        [HttpPost("AddAccount")]
        public async Task AddNewAccount([FromBody]AccountPowerToFlyDto account)
        {
            AccountPowerToFly accountPowerToFly = new AccountPowerToFly
            {
                Email = account.Email,
                Password = account.Password,
                SiteId = account.SiteId,
                Status = Models.Enum.AccountStatus.Available
            };

            Context.AccountsPowerToFly.Add(accountPowerToFly);

            await Context.SaveChangesAsync();
        }

        [HttpPost("ParseByLinks")]
        public async Task<IActionResult> ParseByJobLinks([FromBody]ParseByJobLinkDto parseByJobLinkDto)
        {
            if (parseByJobLinkDto.IgnoreAlreadySended)
            {
                await RemoveAlreadySentedJob(parseByJobLinkDto);
            }

            var account = await Context.AccountsPowerToFly.FirstAsync(x => x.Email == parseByJobLinkDto.AccountEmail);

            if (account == null)
            {
                return new BadRequestObjectResult(new string("Account not found"));
            }

            var site = await Context.Sites.FirstAsync(x => x.Name == SiteName);

            if (site == null)
            {
                return new BadRequestObjectResult(new string("Site not found"));
            }

            var botSignals = new BotSignalPowerToFly
            {
                Account = account,
                BotTypeSignal = BotTypeSignal.SendCoverLetterToJobsByLinks,
                IgnoreAlreadySended = parseByJobLinkDto.IgnoreAlreadySended,
                JobLinks = parseByJobLinkDto.JobLinks.Trim(),
                CoverrLetter = parseByJobLinkDto.CoverLetter,
                Status = BotSignalStatus.Waiting
            };

            Context.BotSignalsPowerToFly.Add(botSignals);
            await Context.SaveChangesAsync();

            return new OkObjectResult(new string("Signal created"));
        }

        [HttpGet("BotSignal")]
        public async Task<IActionResult> GetBotSignal()
        {
            var site = Context.Sites.First(x => x.Name == SiteName);

            if (site == null)
            {
                return new BadRequestObjectResult(new string("Site not found"));
            }

            var botSignal = Context.BotSignalsPowerToFly.Include(x => x.Account).FirstOrDefault(x => x.Account.SiteId == site.Id && x.Status == BotSignalStatus.Waiting);
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

        [HttpGet("ChangeStatus")]
        public async Task ChangeAccountStatus([FromQuery]string email, [FromQuery]string status)
        {
            var account = await Context.AccountsPowerToFly.FirstAsync(x => x.Email == email);
            account.Status = (AccountStatus)Convert.ToInt32(status);
            await Context.SaveChangesAsync();
        }

        [HttpPost("SaveJob")]
        public async Task SaveJob([FromBody]JobDto job)
        {
            Context.JobsPowerToFly.Add(
                new JobPowerToFly { 
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

            List<string> existedJobs = await Context.BotSignalsPowerToFly
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

        public class AccountPowerToFlyDto
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
    }
}