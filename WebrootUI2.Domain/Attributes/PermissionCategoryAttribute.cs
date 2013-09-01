using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebrootUI2.Domain;

namespace WebrootUI2.Domain.Attributes
{
    class PermissionCategoryAttribute : Attribute
    {
        public string Category;

        public PermissionCategoryAttribute(Common.PermissionCategory category)
        {
            Category = category.ToString();
        }
    }
}
