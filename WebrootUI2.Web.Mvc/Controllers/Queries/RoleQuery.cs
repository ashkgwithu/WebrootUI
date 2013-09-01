using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpArch.NHibernate;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;
using WebrootUI2.Web.Mvc.Controllers.ViewModels;
using WebrootUI2.Domain;

namespace WebrootUI2.Web.Mvc.Controllers.Queries
{
    public class RoleQuery : NHibernateQuery
    {
        public List<Role> GetAdminRoles()
        {
            try
            {
                return Session.QueryOver<Role>().List().ToList<Role>();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<Role>();
            }
        }

        /// <summary>
        /// Get Administrator roles for Roles dropdown
        /// </summary>
        public List<SelectListItem> GetDroDownAdminRoles()
        {
            try
            {
                if (Setting.AdministratorId == null)
                    return new List<SelectListItem>();

                return Session.QueryOver<Role>()
                    .Where(r => r.UserCategory != null && r.UserCategory.AdministratorId == Setting.AdministratorId).List()
                    .Select(r => new SelectListItem() { Value = r.RoleId.ToString(), Text = r.Name }).ToList<SelectListItem>();

            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<SelectListItem>();
            }
        }

        public List<RoleModel> GetAdminRoleModels()
        {
            var roleModels = new List<RoleModel>();

            try
            {
                var roles = Session.QueryOver<Role>().Where(r => r.UserCategory.AdministratorId == Setting.AdministratorId).List();

                foreach (Role role in roles)
                {
                    var roleModel = new RoleModel();

                    roleModel.Name = role.Name;

                    foreach (Permission permission in role.Permissions)
                    {
                        roleModel.Permissions.Add(permission.Name);
                    }

                    roleModels.Add(roleModel);
                }

                return roleModels;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<RoleModel>();
            }
        }

        public List<RoleModel> GetRolesFromName(string name)
        {
            try
            {
                return GetAdminRoleModels().Where(r=>r.Name.ToLower().Contains(name.ToLower())).ToList<RoleModel>();
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<RoleModel>();
            }
        }

        public Role GetRoleFromName(string name)
        {
            try
            {
                return GetAdminRoles().Single(r=>r.Name.ToLower() == name.ToLower());
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new Role();
            }
        }

        public string GetRoleNameFromId(Guid roleId)
        {
            var roleName = string.Empty;

            try
            {
                roleName = Session.QueryOver<Role>().Where(r => r.RoleId == roleId).SingleOrDefault().Name;

                return roleName;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return string.Empty;
            }
        }

        public bool IsRoleNameExist(string name)
        {             
            try
            {
                var role = GetAdminRoles().SingleOrDefault(r=>r.Name.ToLower() == name.ToLower());

                if (role == null)
                    return false;

               return true;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return true;
            }
        }

        public List<PermissionModel> GetAdminPermissions()
        {
            var permissions = new List<PermissionModel>();

            try
            {
                Array arrPermisions = Enum.GetValues(typeof(Common.AdminPermission));

                foreach (Common.AdminPermission permission in arrPermisions)
                {
                    permissions.Add(new PermissionModel() { 
                        PermissionId = Common.PermissionId(permission),
                        DisplayName = Common.DisplayName(permission),
                        Category = Common.Category(permission)
                    });
                }

                return permissions;
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                return new List<PermissionModel>();
            }
        }
    }
}