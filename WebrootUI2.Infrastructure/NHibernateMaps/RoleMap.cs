using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            this.Table("aspnet_Roles");
            this.Id(r => r.RoleId, "RoleId");

            this.Map(r => r.Name, "RoleName");
            this.Map(r => r.LoweredName, "LoweredRoleName");

            this.Component(r => r.Audit);
            this.Component(r=>r.UserCategory);

            this.HasMany(r=>r.PermissionsInRoles).KeyColumn("RoleId");
            this.HasMany(r => r.Users).KeyColumn("UserId");

            this.HasManyToMany<Permission>(r => r.Permissions).Table("u_PermissionsInRoles").ParentKeyColumn("RoleId").ChildKeyColumn("PermissionId");
        }
    }
}
