using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain.Attributes;

namespace WebrootUI2.Domain
{
    public static class Common
    {
        //Permission Category Enum
        public enum PermissionCategory
        {
            SuperAdmin,
            System,
            Transactions,
            Accounts,
            Inovice,
            Reports,
            Other,
        }

        //Permission Enum
        public enum AdminPermission
        {
            //Super Admin
            [PermissionId("50868CC7-DF39-44A2-AC3A-C88B31472513")]
            [PermissionCategory(PermissionCategory.SuperAdmin)]
            [PermissionDisplayName("Super Admin Permission")]
            SuperAdminPermission,

            //System
            [PermissionId("FC5C37EE-2C0C-48A0-ACA7-886676C00D6F")]
            [PermissionCategory(PermissionCategory.System)]
            [PermissionDisplayName("Edit System Settings")]
            EditSystemSettings,

            [PermissionId("772FF2D4-A0D2-4537-BE3C-9239D16E9A33")]
            [PermissionCategory(PermissionCategory.Other)]
            [PermissionDisplayName("View Transactions")]
            ViewTransactions

        }

        /// <summary>
        /// Get Permission Id
        /// </summary>
        public static Guid PermissionId(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            PermissionIdAttribute attribute = (PermissionIdAttribute)fi.GetCustomAttribute(
                typeof(PermissionIdAttribute), false);

            return attribute.PermissionId;
        }

        /// <summary>
        /// Get Permission category
        /// </summary>
        public static string Category(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            PermissionCategoryAttribute attribute = (PermissionCategoryAttribute)fi.GetCustomAttribute(
                typeof(PermissionCategoryAttribute), false);

            return attribute.Category;
        }

        /// <summary>
        /// Get Permission display name
        /// </summary>
        public static string DisplayName(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            PermissionDisplayNameAttribute attribute = (PermissionDisplayNameAttribute)fi.GetCustomAttribute(
                typeof(PermissionDisplayNameAttribute), false);

            return attribute.DisplayName;
        }

        /// <summary>
        /// Get PermissionCategory enum values
        /// </summary>
        public static Array GetPermissionCategories()
        {
            return Enum.GetValues(typeof(PermissionCategory));
        }
    }
}
