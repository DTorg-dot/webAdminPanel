using System.Collections.Generic;
using WebAdminPanel.Models.Enum;

namespace WebAdminPanel.Models.Base
{
    public class AccountBase
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AccountStatus Status { get; set; }
    }
}
