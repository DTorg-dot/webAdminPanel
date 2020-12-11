using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminPanel.Models.Base
{
    public class JobBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string CoverLetter { get; set; }

        public int AccountId { get; set; }

        public AccountBase Account { get; set; }
    }
}
