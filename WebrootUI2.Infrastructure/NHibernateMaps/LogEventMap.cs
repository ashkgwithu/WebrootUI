using FluentNHibernate.Mapping;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class LogEventMap: ClassMap<LogEvent>
    {
        public LogEventMap()
        {
            Table("s_EventLog");
            Id(x => x.Id).Column("Id");
            Map(x => x.Date).Column("[Timestamp]").Not.Nullable();
            Map(x => x.Level).Column("[Level]").Not.Nullable();
            Map(x => x.Message).Column("[Message]").Not.Nullable();
            References(x => x.User).Column("[UserId]");
        }
    }
}
