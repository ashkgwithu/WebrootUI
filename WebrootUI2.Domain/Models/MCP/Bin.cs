using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Models.MCP
{
    public class Bin : Entity
    {
        public virtual int _Bin { get; set; }
        public virtual int CardNumberLength { get; set; }
        public virtual string Description { get; set; }
        public virtual int Range { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
    }
}
