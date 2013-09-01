using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Attributes
{
    class PermissionDisplayNameAttribute : Attribute
    {
        public string DisplayName;

        public PermissionDisplayNameAttribute(string name)
        {
            DisplayName = name;
        }
    }
}
