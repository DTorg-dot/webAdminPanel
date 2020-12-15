using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminPanel.Models
{
    public class DbInitializer
    {
        private readonly DatabaseContext Context;

        public DbInitializer(DatabaseContext context)
        {
            this.Context = context;
        }
        public async Task Initialize()
        {
            bool isDbFresh = Context.Database.EnsureCreated();

            if (!isDbFresh)
            {
                await SeedData();
            }
        }

        private async Task SeedData()
        {
            Context.Database.EnsureCreated();

            var isSiteExist = Context.SitePowerToFly.Any(x => x.Name == "Powertofly");

            if (!isSiteExist)
            {
                Context.SitePowerToFly.Add(new Models.PowerToFly.SitePowerToFly { Name = "Powertofly" });
            }

            await Context.SaveChangesAsync();
        }

    }
}
