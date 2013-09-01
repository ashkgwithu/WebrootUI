using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebrootUI2.Web.Mvc.Controllers.ViewModels
{
    public class PermissionModel
    {
        public Guid PermissionId { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
    }
}