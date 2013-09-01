using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class TerminalMap : ClassMap<Terminal>
    {
        public TerminalMap()
        {
            this.Table("u_Terminal");
            this.Id(t=>t.TerminalId,"TerminalId");
            this.Map(t=>t.Name,"Name");
            this.References(t=>t.Merchant).Column("MerchantId");
        }
    }
}
