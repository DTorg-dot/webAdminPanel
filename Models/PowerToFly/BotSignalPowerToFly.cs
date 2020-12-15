using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminPanel.Models.PowerToFly
{
    public class BotSignalPowerToFly : BotSignal
    {
        public int AccountId { get; set; }

        public AccountPowerToFly Account { get; set; }

        public ICollection<JobPowerToFly> Jobs { get; set; }
    }
}
