﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models.PowerToFly
{
    public class JobPowerToFly : JobBase
    {
        public int SignalId { get; set; }

        public BotSignalPowerToFly Signal { get; set; }
    }
}
