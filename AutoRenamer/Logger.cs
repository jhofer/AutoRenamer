using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRenamer
{
    public class Logger :ILogger
    {
        private readonly EventLog logger;

        public Logger()
        {
            if (!EventLog.SourceExists("AutoRenamer"))
            {
                EventLog.CreateEventSource("AutoRenamer", "AutoRenamer");
            }
            this.logger = new EventLog();
            logger.Source = "AutoRenamer";
        }

        public void Error(Exception e)
        {
            logger.WriteEntry(e.Message + " " + e.StackTrace, EventLogEntryType.Error);
        }

        public void Info(string str)
        {
            logger.WriteEntry(str, EventLogEntryType.Information);
        }

        public void Error(string str)
        {
            logger.WriteEntry(str, EventLogEntryType.Error);
        }
    }
}
