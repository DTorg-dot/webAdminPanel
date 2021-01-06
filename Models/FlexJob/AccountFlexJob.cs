using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models.FlexJob
{
    public class AccountFlexJob : AccountBase
    {
        public SiteFlexJob Site { get; set; }

        public int SiteId { get; set; }

        public ICollection<BotSignalFlexJob> Signals { get; set; }
    }
}
