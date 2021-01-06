using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;
using WebAdminPanel.Models.FlexJob;
using WebAdminPanel.Models.PowerToFly;

namespace WebAdminPanel.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        { }

        #region BaseTables
        public DbSet<AccountBase> Accounts { get; set; }

        public DbSet<JobBase> Jobs { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<BotSignal> BotSignals { get; set; }
        #endregion

        #region PowerToFly
        public DbSet<AccountPowerToFly> AccountsPowerToFly { get; set; }

        public DbSet<JobPowerToFly> JobsPowerToFly { get; set; }

        public DbSet<SitePowerToFly> SitePowerToFly { get; set; }

        public DbSet<BotSignalPowerToFly> BotSignalsPowerToFly { get; set; }
        #endregion

        #region FlexJob
        public DbSet<AccountFlexJob> AccountsFlexJob { get; set; }

        public DbSet<JobFlexJob> JobsFlexJob { get; set; }

        public DbSet<SiteFlexJob> SiteFlexJob { get; set; }

        public DbSet<BotSignalFlexJob> BotSignalFlexJob { get; set; }
        #endregion
    }
}
