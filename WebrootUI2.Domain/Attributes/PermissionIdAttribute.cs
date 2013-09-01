using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebrootUI2.Domain.Attributes
{
    class PermissionIdAttribute : Attribute
    {
        public Guid PermissionId;

        public PermissionIdAttribute(string id)
        {
            PermissionId = new Guid(id);
        }
    }
}
