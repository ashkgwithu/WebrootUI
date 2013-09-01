using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Domain.Models;
using WebrootUI2.Infrastructure.Common.Log;
using WebrootUI2.Web.Mvc.Controllers.Queries;
using WebrootUI2.Web.Mvc.Controllers.ViewModels;
using WebrootUI2.Web.Mvc.Filters;
using WebrootUI2.Domain;

namespace WebrootUI2.Web.Mvc.Controllers
{
    [Authorize]
    [Permissions(AllowedPermissions = new Common.AdminPermission[]{
    Common.AdminPermission.EditSystemSettings})]
    public class AcquireController : Controller
    {
        private readonly ILogEventTask logEventTask;
        private readonly IUserTask userTask;
        private readonly IRoleTask roleTask;
        private readonly RoleQuery roleQuery;
        private readonly UserQuery userQuery;
        //
        // GET: /Acquire/

        public ActionResult Index()
        {
            var auditModel = new AuditModel() { Username = string.Empty, RoleName = string.Empty };

            if (Request.QueryString["isCa"] != null && Request.QueryString["isCa"] == "true" && HttpContext.Cache["auditModel"] != null)
            {
                var cachedAuditModel = (AuditModel)HttpContext.Cache["auditModel"];

                auditModel.Username = cachedAuditModel.Username;
                auditModel.RoleName = cachedAuditModel.RoleName;
                auditModel.TotalRecordsCount = cachedAuditModel.Users.Count;
                auditModel.Users = cachedAuditModel.Users.Take(Setting.Page_Size).ToList<UserModel>();
            }
            else
            {
                var users = new List<UserModel>();

                foreach (var user in userTask.GetAdminUsers())
                {
                    users.Add(new UserModel()
                    {
                        Name = user.UserName,
                        RoleName = user.Role.Name,
                        Id = user.UserId
                    });
                }

                auditModel.TotalRecordsCount = users.Count();
                auditModel.Users = users.Take(Setting.Page_Size).ToList<UserModel>();
                HttpContext.Cache["auditModel"] = new AuditModel() { Users = users };
            }

            return View(auditModel);
        }

        //
        // GET: /Acquire/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Acquire/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Acquire/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Acquire/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Acquire/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Acquire/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Acquire/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #region Acquire list

       

        /// <summary>
        /// Search Users in the Audit 
        /// </summary>
        [HttpGet]
        public JsonResult AcquireUsersSearch(string username, string role)
        {

            var users = new List<UserModel>();
            var count = 0;

            var usersQuery = userTask.Search(username == null ? string.Empty : username, role == null ? string.Empty : role);

            foreach (var user in usersQuery)
            {
                users.Add(new UserModel()
                {
                    Name = user.UserName,
                    RoleName = user.Role.Name,
                    Id = user.UserId
                });
            }

            HttpContext.Cache["auditModel"] = new AuditModel() { Username = username, RoleName = role, Users = users };
            count = users.Count;

            return Json(new { status = "success", userList = users.Take(Setting.Page_Size).ToList<UserModel>(), recordsCount = count }
                , JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Fetch data from the cached users list in the page index change
        /// </summary>
        [HttpGet]
        public JsonResult AcquirePagingIndexChanged(int index)
        {
            var count = 0;
            var users = new List<UserModel>();
            var cachedAuditModel = (AuditModel)HttpContext.Cache["auditModel"];

            if (cachedAuditModel == null)
                return Json(new { status = "failed" });

            users = cachedAuditModel.Users.Skip((index - 1) * Setting.Page_Size).Take(Setting.Page_Size).ToList<UserModel>();
            count = cachedAuditModel.Users.Count;

            return Json(new { status = "success", usersList = users, currentIndex = index, recordsCount = count }, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
}
