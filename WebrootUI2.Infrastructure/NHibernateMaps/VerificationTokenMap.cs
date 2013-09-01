using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class VerificationTokenMap : ClassMap<VerificationToken>
    {
        public VerificationTokenMap()
        {
            this.Table("u_VerificationToken");

            this.Id().GeneratedBy.Identity().Column("TokenId");
            this.Map(l=>l.Token,"Token");
            this.Map(l=>l.CreatedDate,"CreatedDate");
            this.Map(l => l.UserId, "UserId");
        }
    }
}
