using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Domain.Contracts.Tasks
{
    public interface ILogEventTask
    {
        List<LogEvent> GetLogByUserId(Guid userId);

        List<LogEvent> Search(DateTime? from, DateTime? to, string level, string message,Guid userId);
    }
}
