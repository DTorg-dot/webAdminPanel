using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models.FlexJob
{
    public class SiteFlexJob : Site
    {
        public ICollection<AccountFlexJob> Accounts { get; set; }
    }
}
