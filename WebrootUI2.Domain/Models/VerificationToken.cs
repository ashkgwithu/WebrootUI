using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace WebrootUI2.Domain.Models
{
    public class VerificationToken : Entity
    {
        public virtual Guid Token { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual Guid UserId { get; set; }
    }
}
