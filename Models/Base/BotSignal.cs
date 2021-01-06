using System.Collections.Generic;
using WebAdminPanel.Models.Base;

namespace WebAdminPanel.Models
{
    public enum BotTypeSignal
    {
        GetJobsByLinks = 1,
        SendCoverLetterToJobsByLinks = 2
    }

    public enum BotSignalStatus
    {
        Waiting = 1,
        InProgress = 2,
        Finished = 3
    }

    public class BotSignal
    {
        public int Id { get; set; }

        public string JobLinks { get; set; }

        public string CoverrLetter { get; set; }

        public BotTypeSignal BotTypeSignal { get; set; }

        /// <summary>
        /// True - Do not send cover letter to job if another account has already sent
        /// False - Send cover letter to job if another account has already sent
        /// </summary>
        public bool IgnoreAlreadySended { get; set; }

        public BotSignalStatus Status { get; set; }

        public int MaxPageCount { get; set; }
    }
}
