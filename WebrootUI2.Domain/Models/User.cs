using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;
using System.Web.Script.Serialization;

namespace WebrootUI2.Domain.Models
{
    public class User : Entity
    {
        public User()
        {
            LogEvents = new List<LogEvent>();
        }

        public virtual Guid UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string LoweredUserName { get; set; }
        public virtual Role Role { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordQuestion { get; set; }
        public virtual bool IsLockedOut { get; set; }
        public virtual DateTime LastActivityDate { get; set; }
        public virtual DateTime LastLoginDate { get; set; }
        public virtual AuditComponent Audit { get; set; }
        public virtual UserCategoryComponent UserCategory { get; set; }

        [ScriptIgnore]
        public virtual IList<LogEvent> LogEvents { get; set; }
    }
}