using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebrootUI2.Domain.Models
{
    public class Role : Entity
    {
        public Role()
        {
            Users = new List<User>();
            Permissions = new List<Permission>();
            PermissionsInRoles = new List<PermissionsInRoles>();
        }

        public virtual Guid RoleId { get; set; }
        public virtual string Name { get; set; }
        public virtual string LoweredName { get; set; }
        public virtual AuditComponent Audit { get; set; }
        public virtual UserCategoryComponent UserCategory { get; set; }

        [ScriptIgnore]
        public virtual IList<User> Users { get; set; }

        [ScriptIgnore]
        public virtual IList<Permission> Permissions { get; set; }

        [ScriptIgnore]
        public virtual IList<PermissionsInRoles> PermissionsInRoles { get; set; }

        public virtual void AddPermissionsInRoles(PermissionsInRoles pr)
        {
            PermissionsInRoles.Add(pr);
        }
    }
}
