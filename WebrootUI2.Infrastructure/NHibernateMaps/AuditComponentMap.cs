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
    class AuditComponentMap : ComponentMap<AuditComponent>
    {
        public AuditComponentMap()
        {
            this.Map(b => b.CreatedById).Column("CreatedBy");
            this.Map(b => b.LastModifiedById).Column("LastModifiedBy").Nullable();
            this.Map(b => b.IsDeleted).Column("IsDeleted");
            this.Map(b => b.CreatedDate).Column("CreatedDate");
            this.Map(b => b.LastModifiedDate).Column("LastModifiedDate").Nullable();
        }
    }
}