using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Infrastructure.NHibernateMaps
{
    class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            this.Table("aspnet_Users");
            this.Id(u => u.UserId, "UserId");
            this.Map(u => u.UserName, "UserName");
            this.Map(u => u.LoweredUserName, "LoweredUserName");
            this.Map(u => u.LastActivityDate, "LastActivityDate");

            this.Component(u => u.Audit);
            this.Component(u => u.UserCategory);

            this.References(u => u.Role).Column("RoleId");
            HasMany(x => x.LogEvents).Table("[s_EventLog]");
        }
    }
}
