using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models.MCP;

namespace WebrootUI2.Infrastructure.NHibernateMaps.MCP
{
    class BinMap : ClassMap<Bin>
    {
        public BinMap()
        {
            this.Table("MCP_Bin");
            this.Id().Column("Id").GeneratedBy.Identity();
            this.Map(b => b._Bin, "Bin");
            this.Map(b => b.CardNumberLength, "CardNumberLength");
            this.Map(b=>b.Description,"Description");
            this.Map(b => b.Range, "Range");
            this.Map(b => b.CreatedDate, "CreatedDate");
            this.Map(b=>b.CreatedBy,"CreatedBy");
            this.Map(b => b.ModifiedDate, "ModifiedDate");
            this.Map(b => b.ModifiedBy,"ModifiedBy");
        }
    }
}
