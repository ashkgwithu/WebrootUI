using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class MerchantMap : ClassMap<Merchant>
    {
        public MerchantMap()
        {
            this.Table("u_Merchant");
            this.Id(m=>m.MerchantId,"MerchantId");
            this.Map(m=>m.Name,"Name");
            this.References(m=>m.MasterMerchant).Column("MasterMerchantId");
        }
    }
}
