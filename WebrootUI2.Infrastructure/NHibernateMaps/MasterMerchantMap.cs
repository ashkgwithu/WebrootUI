using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class MasterMerchantMap : ClassMap<MasterMerchant>
    {
        public MasterMerchantMap()
        {
            this.Table("u_MasterMerchant");
            this.Id(m => m.MasterMerchantId, "MasterMerchantId");
            this.Map(m=>m.Name,"Name");
        }
    }
}
