using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Models
{
    public class Merchant : Entity
    {
        public virtual Guid MerchantId { get; set; }
        public virtual string Name { get; set; }
        public virtual MasterMerchant MasterMerchant { get; set; }
    }
}
