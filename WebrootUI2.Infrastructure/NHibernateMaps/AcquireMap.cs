using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;


namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class AcquireMap : ClassMap<Acquire>
    {
        public AcquireMap()
        {
            this.Table("MCP_Acquirer");
           //this.Id().Column("Id").GeneratedBy.Identity();
            this.Id(u => u.Id,"Id").GeneratedBy.Identity();
            //this.Map(u => u.Id).Column("Id");
            //this.Map(u => u.Id, "Id");
            this.Map(u => u.name, "name");
            this.Map(u => u.LogicalId, "LogicalId");
            this.Map(u => u.Enabled, "Enabled");
            this.Map(u => u.CreatedBy, "CreatedBy");
            this.Map(u => u.CreatedDate, "CreatedDate");
            this.Map(u => u.LastModifiedBy, "LastModifiedBy");
            this.Map(u => u.LastModifiedDate, "LastModifiedDate");
            this.Map(u => u.IsDeleted, "IsDeleted");
            //this.Map(u => u.LastActivityDate, "LastActivityDate");

            //this.Component(u => u.Audit);
            //this.Component(u => u.UserCategory);  
            HasMany(x => x.LogEvents).Table("[s_EventLog]");
        }
    }
}
