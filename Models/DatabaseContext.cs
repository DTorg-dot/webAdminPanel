using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;
using WebAdminPanel.Models.PowerToFly;

namespace WebAdminPanel.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        { }

        public DbSet<AccountPowerToFly> AccountsPowerToFly { get; set; }

        public DbSet<JobPowerToFly> JobsPowerToFly { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<BotSignal> BotSignals { get; set; }
    }
}
