using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpArch.Domain.DomainModel;
using System.Web.Script.Serialization;

namespace WebrootUI2.Domain.Models
{
    public class Acquire : Entity
    {
        public Acquire()
        {
            LogEvents = new List<LogEvent>();
        }
        public virtual int _Acquire { get; set; }
        public virtual int  Id { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual int LogicalId { get; set; }
        public virtual string name { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime LastModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string LastModifiedBy { get; set; }
        [ScriptIgnore]
        public virtual IList<LogEvent> LogEvents { get; set; }

    }
}
