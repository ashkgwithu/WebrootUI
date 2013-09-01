using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Models.MCP
{
    public class Module : Entity
    {
        public virtual string Description { get; set; }
        public virtual string _Module { get; set; }
        public virtual AuditComponent Audit { get; set; }
    }
}
