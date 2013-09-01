using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Models
{
    public class Permission : Entity
    {
        public virtual Guid PermissionId { get; set; }
        public virtual string Name { get; set; }
    }
}