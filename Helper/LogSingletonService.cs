using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminPanel.Helper
{
    public class LogSingletonService
    {
        public IList<string> PowerToFlyStorage { get; set; } = new List<string> ();

        public void AddToStorage(string message)
        {
            if (PowerToFlyStorage.Count > 50)
            {
                PowerToFlyStorage.Clear();
            }
            PowerToFlyStorage.Add(message);
        }

        public IList<string> GetFromStorage()
        {
            var tempStorage = PowerToFlyStorage.ToList();
            PowerToFlyStorage.Clear();
            return tempStorage;
        }
    }
}
