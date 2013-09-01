using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Web.Mvc.Controllers.ViewModels
{
    public class RoleModel
    {
        public RoleModel()
        {
            Permissions = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}