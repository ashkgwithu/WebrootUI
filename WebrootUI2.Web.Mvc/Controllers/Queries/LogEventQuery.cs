using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Web.Mvc.Controllers.Queries
{
    public class LogEventQuery
    {
        private ILogEventTask logEventTask;

        public LogEventQuery(ILogEventTask eventTask)
        {
            this.logEventTask = eventTask;
        }

        public IEnumerable<LogEvent> GetLogEventsByUserId(Guid userId)
        {
            IEnumerable<LogEvent> events = logEventTask.GetLogByUserId(userId);
            return events;
        }
    }
}