using Microsoft.EntityFrameworkCore;
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
                Context.Database.Migrate();
                await SeedData();
            }
        }

        private async Task SeedData()
        {
            var isSiteExist = Context.SitePowerToFly.Any(x => x.Name == "Powertofly");

            if (!isSiteExist)
            {
                Context.SitePowerToFly.Add(new Models.PowerToFly.SitePowerToFly { Name = "Powertofly" });
            }

            isSiteExist = Context.SitePowerToFly.Any(x => x.Name == "FlexJob");

            if (!isSiteExist)
            {
                Context.SiteFlexJob.Add(new Models.FlexJob.SiteFlexJob { Name = "FlexJob" });
            }

            await Context.SaveChangesAsync();
        }

    }
}
