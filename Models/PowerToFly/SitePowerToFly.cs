using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models.PowerToFly
{
    public class SitePowerToFly : Site
    {
        public ICollection<AccountPowerToFly> Accounts { get; set; }
    }
}
