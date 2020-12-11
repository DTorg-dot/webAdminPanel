using System.Collections.Generic;

namespace WebAdminPanel.Models.Base
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AccountBase> Accounts { get; set; }
    }
}
