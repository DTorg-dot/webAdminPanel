using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminPanel.Models.FlexJob
{
    public class BotSignalFlexJob : BotSignal
    {
        public int AccountId { get; set; }

        public AccountFlexJob Account { get; set; }

        public int ProfileId { get; set; }

        public ICollection<JobFlexJob> Jobs { get; set; }
    }
}
