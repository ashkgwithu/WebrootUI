using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;
using WebrootUI2.Web.Mvc.Controllers.Queries;
using WebrootUI2.Web.Mvc.Providers.Queries;

namespace WebrootUI2.Web.Mvc.Providers
{
    public static class SecurityProvider
    {
        private static SecurityQuery SecurityQuery = new SecurityQuery();

        public static bool HasPermission(string username, Common.AdminPermission[] AllowedPermissions)
        {
            try
            {
                var role = SecurityQuery.GetRoleByUsername(username);

                //Return true if the user is a Super Admin
                if (role.Permissions.Any(r => r.Name.ToLower() == Common.AdminPermission.SuperAdminPermission.ToString().ToLower()))
                    return true;

                foreach (var permission in AllowedPermissions)
                {
                    if (role.Permissions.Any(r => r.Name.ToLower() == permission.ToString().ToLower()))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return false;
            }
        }
    }
}