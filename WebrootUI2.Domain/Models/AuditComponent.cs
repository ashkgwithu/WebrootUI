using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Models
{
    public class AuditComponent
    {
        public virtual Guid CreatedById { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual Guid? LastModifiedById { get; set; }
        public virtual DateTime? LastModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}