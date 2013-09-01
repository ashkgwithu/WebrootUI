using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SharpArch.Domain.PersistenceSupport;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Tasks
{
    public class LogEventTask : ILogEventTask
    {
        private readonly IRepository<LogEvent> logEventRepo;

        public LogEventTask(IRepository<LogEvent> logEventRepo)
        {
            this.logEventRepo = logEventRepo;
        }

        public List<LogEvent> GetLogByUserId(Guid userId)
        {
            List<LogEvent> events = logEventRepo.GetAll().ToList<LogEvent>();
            return events.Where(logEvent => logEvent.User.UserId == userId).ToList();
        }

        public List<LogEvent> Search(DateTime? _from, DateTime? to, string level, string message,Guid userId)
        {
            var logEvents = new List<LogEvent>();

            try
            {
                logEventRepo.DbContext.BeginTransaction();

                logEvents = (from l in logEventRepo.GetAll()
                             where
                                 (
                                 (l.User.UserId == userId) && 
                                 (_from == null || l.Date.Date >= _from) &&
                                 (to == null || l.Date.Date <= to) &&
                                 (level == string.Empty || l.Level.ToLower().Contains(level.ToLower())) &&
                                 (message == string.Empty || l.Message.ToLower().Contains(message.ToLower())))
                             select l).ToList<LogEvent>();

                logEventRepo.DbContext.CommitTransaction();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<LogEvent>();
            }

            return logEvents;
        }
    }
}
