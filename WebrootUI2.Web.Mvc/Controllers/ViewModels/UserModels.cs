using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebrootUI2.Domain.Models;

namespace WebrootUI2.Web.Mvc.Controllers.ViewModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid CompanyId { get; set; }
        public string Company { get; set; }
        public bool IsDeleted { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
    }
}