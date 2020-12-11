using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models;
using WebAdminPanel.Models.Base;
using WebAdminPanel.Models.PowerToFly;

namespace WebAdminPanel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public DatabaseContext Context { get; set; }

        public AccountController(DatabaseContext context)
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
    }

    public class AccountPowerToFlyDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int SiteId { get; set; }
    }
}
