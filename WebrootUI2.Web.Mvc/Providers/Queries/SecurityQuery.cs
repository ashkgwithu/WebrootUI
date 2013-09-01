using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;

namespace WebrootUI2.Web.Mvc.Providers.Queries
{
    public class SecurityQuery : NHibernateQuery
    {
        public Role GetRoleByUsername(string username)
        {
            try
            {
                return Session.QueryOver<User>().Where(u => u.UserName == username.ToLower()).SingleOrDefault().Role;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new Role();
            }
        }
    }
}