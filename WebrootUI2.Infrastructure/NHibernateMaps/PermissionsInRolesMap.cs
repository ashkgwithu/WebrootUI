using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class PermissionsInRolesMap : ClassMap<PermissionsInRoles>
    {
        public PermissionsInRolesMap()
        {
            this.Table("u_PermissionsInRoles");
            this.CompositeId()
                .KeyProperty(p => p.PermissionId, "PermissionId")
                .KeyProperty(p => p.RoleId, "RoleId");
        }
    }
}
