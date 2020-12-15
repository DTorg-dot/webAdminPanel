using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models.PowerToFly
{
    public class AccountPowerToFly : AccountBase
    {
        public SitePowerToFly Site { get; set; }

        public int SiteId { get; set; }

        public ICollection<BotSignalPowerToFly> Signals { get; set; }
    }
}
