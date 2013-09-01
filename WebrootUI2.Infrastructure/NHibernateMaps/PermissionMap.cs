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
    class PermissionMap : ClassMap<Permission>
    {
        public PermissionMap()
        {
            this.Table("u_Permissions");
            this.Id(p => p.PermissionId).Column("PermissionId");
            this.Map(p => p.Name).Column("Name");
        }
    }
}
