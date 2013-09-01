using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace WebrootUI2.Infrastructure.Common.Log
{
    public class LogManager
    {
        private static Logger log = NLog.LogManager.GetCurrentClassLogger();

        //Log system events
        public static void Log(string message, LogType logType, Guid userId)
        {
            LogEventInfo _event;

            switch (logType)
            {
                case LogType.info:
                    _event = new LogEventInfo(LogLevel.Info, "", message);
                    break;
                case LogType.error:
                    _event = new LogEventInfo(LogLevel.Error, "", message);
                    break;
                case LogType.warning:
                    _event = new LogEventInfo(LogLevel.Warn, "", message);
                    break;
                default:
                    _event = new LogEventInfo(LogLevel.Error, "", message);
                    break;
            }

            _event.Properties["UserId"] = userId;
            log.Log(_event);
        }

        //Log exceptions
        public static void LogException(Exception ex)
        {
            log.LogException(LogLevel.Debug, "Exception", ex);
        }
    }
}
