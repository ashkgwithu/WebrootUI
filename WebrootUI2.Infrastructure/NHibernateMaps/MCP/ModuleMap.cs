using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models.MCP;

namespace WebrootUI2.Infrastructure.NHibernateMaps.MCP
{
    class ModuleMap : ClassMap<Module>
    {
        public ModuleMap()
        {
            this.Table("MCP_Module");
            this.Id().Column("Id").GeneratedBy.Identity();
            this.Map(m=>m.Description,"Description");
            this.Map(m=>m._Module,"Module");
            this.Component(m => m.Audit);
        }
    }
}
