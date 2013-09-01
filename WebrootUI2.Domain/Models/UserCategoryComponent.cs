using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Models
{
    public class UserCategoryComponent
    {
        public virtual Guid? AdministratorId { get; set; }
        public virtual MasterMerchant MasterMerchant { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual Terminal Terminal { get; set; }
    }
}
