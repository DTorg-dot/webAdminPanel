using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models.FlexJob
{
    public class JobFlexJob : JobBase
    {
        public int SignalId { get; set; }

        public BotSignalFlexJob Signal { get; set; }
    }
}
