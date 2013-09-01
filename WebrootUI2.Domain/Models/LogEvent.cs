using System;
using SharpArch.Domain.DomainModel;

namespace WebrootUI2.Domain.Models
{
    public class LogEvent: Entity 
    {
        public virtual string Level { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual User User { get; set; }
    }
}
