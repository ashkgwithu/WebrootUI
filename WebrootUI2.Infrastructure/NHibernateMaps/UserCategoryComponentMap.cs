using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class UserCategoryComponentMap : ComponentMap<UserCategoryComponent>
    {
        public UserCategoryComponentMap()
        {
            this.Map(u => u.AdministratorId, "AdministratorId").Nullable();
            this.References(u => u.MasterMerchant, "MasterMerchantId").Nullable();
            this.References(u => u.Merchant, "MerchantId").Nullable();
            this.References(u => u.Terminal, "TerminalId").Nullable();
        }
    }
}
