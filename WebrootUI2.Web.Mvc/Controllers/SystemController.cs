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
    public class SystemController : BaseController
    {
        private readonly ILogEventTask logEventTask;
        private readonly IUserTask userTask;
        private readonly IAcquireTask acquireTask;
        private readonly IRoleTask roleTask;
        private readonly RoleQuery roleQuery;
        private readonly UserQuery userQuery;

        public SystemController(ILogEventTask logEventTask, IUserTask userTask, RoleQuery companyRoleQuery,
        UserQuery userQuery, IRoleTask roleTask, IAcquireTask acquireTask)
        {
            this.userTask = userTask;
            this.logEventTask = logEventTask;
            this.roleQuery = companyRoleQuery;
            this.userQuery = userQuery;
            this.roleTask = roleTask;
            this.acquireTask = acquireTask;
        }


        #region Users list

        /// <summary>
        /// Load the Users list in the Audit view.
        /// </summary>
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

        /// <summary>
        /// Search Users in the Audit 
        /// </summary>
        [HttpGet]
        public JsonResult UsersSearch(string username, string role)
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
        public JsonResult PagingIndexChanged(int index)
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

        #region Logs

        /// <summary>
        /// Load Audit logs of the selected user.
        /// </summary>
        public ActionResult Audit()
        {
            var userId = Guid.Empty;

            if (Request.QueryString["userId"] == null ||
               !Guid.TryParse(Request.QueryString["userId"].ToString(), out userId))
            {
                TempData["error"] = "Inavlid url";
                return View(new List<LogEvent>());
            }

            Session["userId"] = userId;
            var logs = new List<LogModel>();

            var eventsQuery = logEventTask.GetLogByUserId(userId);

            foreach (var eventLog in eventsQuery)
            {
                logs.Add(new LogModel() { DisplayDate = eventLog.Date.ToString(), Level = eventLog.Level, Message = eventLog.Message });
            }

            HttpContext.Cache["Logs"] = logs;
            ViewBag.recordsCount = logs.Count;

            return View(logs.Take(Setting.Page_Size).ToList<LogModel>());
        }

        /// <summary>
        /// Fetch data from the cached log list in the page index change
        /// </summary>
        [HttpGet]
        public JsonResult AuditPagingIndexChanged(int index)
        {
            var count = 0;
            var logs = new List<LogModel>();

            var cachedLogs = (List<LogModel>)HttpContext.Cache["Logs"];

            if (cachedLogs == null)
                return Json(new { status = "failed" });

            logs = cachedLogs.Skip((index - 1) * Setting.Page_Size).Take(Setting.Page_Size).ToList<LogModel>();
            count = cachedLogs.Count;


            return Json(new { status = "success", logEvents = logs, currentIndex = index, recordsCount = count }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Search audit log events.
        /// </summary>
        [HttpGet]
        public JsonResult AuditSearch(DateTime? from, DateTime? to, string level, string message)
        {
            var userId = Guid.Empty;
            var logs = new List<LogModel>();

            if (!Guid.TryParse(Session["userId"].ToString(), out userId))
                return Json(new { status = "failed" });

            var eventList = logEventTask.Search(from, to, level, message, userId);

            foreach (var _event in eventList)
            {
                logs.Add(new LogModel() { DisplayDate = _event.Date.ToString(), Level = _event.Level, Message = _event.Message });
            }

            HttpContext.Cache["Logs"] = logs;
            return Json(new { status = "success", logEvents = logs.Take(Setting.Page_Size), recordsCount = logs.Count },
                JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Acuire
        public ActionResult Acquire()
        {
            //var acquireModel = new AcquireModel() { Id=0, name = string.Empty, logicalid = 0,Enabled=false };
            //var acquire = new List<AcquireModel>();
            //if (Request.QueryString["isCa"] != null && Request.QueryString["isCa"] == "true" && HttpContext.Cache["acquireModel"] != null)
            //{
            //    var cachedAuditModel = (AcquireModel)HttpContext.Cache["acquireModel"];
            //    acquireModel.Id = cachedAuditModel.Id;
            //    acquireModel.name = cachedAuditModel.name;
            //    acquireModel.logicalid = cachedAuditModel.logicalid;
            //    acquireModel.Enabled = cachedAuditModel.Enabled;
            //    acquireModel.TotalRecordsCount = cachedAuditModel.Acquire.Count; 
            //}
            //else
            //{
              

            //    foreach (var user in acquireTask.GetAll())
            //    {
            //        acquire.Add(new AcquireModel()
            //        {
            //             Id=user.Id,
            //            name = user.name,
            //            logicalid = user.LogicalId,
            //            Enabled = user.Enabled

            //        });
            //    }

            //    acquireModel.TotalRecordsCount = acquire.Count();
            //    acquireModel.Acquire = acquire.Take(Setting.Page_Size).ToList<AcquireModel>();                 
            //    HttpContext.Cache["acquireModel"] = new AcquireModel() { Acquire = acquire  };
            //}

            //return View(acquire.Take(Setting.Page_Size).ToList<AcquireModel>());
            var acquireModel = new AcquireModel() { Id = 0, name = string.Empty, logicalid = 0, Enabled = false };
            var acquires = new List<AcquireModel>();
            if (Request.QueryString["isCa"] != null && Request.QueryString["isCa"] == "true" && HttpContext.Cache["acquireModel"] != null)
            {
                var cachedAcquireModel = (AcquireModel)HttpContext.Cache["acquireModel"];

                acquireModel.Id = cachedAcquireModel.Id;
                acquireModel.name = cachedAcquireModel.name;
                acquireModel.logicalid = cachedAcquireModel.logicalid;
                acquireModel.TotalRecordsCount = cachedAcquireModel.Acquire.Count;
                acquireModel.Acquire = cachedAcquireModel.Acquire.Take(Setting.Page_Size).ToList<AcquireModel>();
            }
            else
            {
                foreach (var user in acquireTask.GetAll())
                {
                    acquires.Add(new AcquireModel()
                    {
                        Id = user.Id,
                                  name = user.name,
                                  logicalid = user.LogicalId,
                                  Enabled = user.Enabled
                    });
                }

                acquireModel.Acquire = acquires.Take(Setting.Page_Size).ToList<AcquireModel>();
                acquireModel.TotalRecordsCount = acquires.Count();             
                HttpContext.Cache["acquireModel"] = new AcquireModel() { Acquire = acquires };
                ViewBag.recordsCount = acquires.Count;
            }

            return View(acquires.Take(Setting.Page_Size).ToList<AcquireModel>());
        }

        /// <summary>
        /// Fetch data from the cached users list in the page index change
        /// </summary>
        [HttpGet]
        public JsonResult AcuirePagingIndexChanged(int index)
        {
            //var count = 0;
            //var users = new List<AcquireModel>();
            //var cachedAuditModel = (AcquireModel)HttpContext.Cache["acquireModel"];

            //if (cachedAuditModel == null)
            //    return Json(new { status = "failed" });

            //users = cachedAuditModel.Acquire.Skip((index - 1) * Setting.Page_Size).Take(Setting.Page_Size).ToList<AcquireModel>();
            //count = cachedAuditModel.Acquire.Count;

            //return Json(new { status = "success", usersList = users, currentIndex = index, recordsCount = count }, JsonRequestBehavior.AllowGet);
            var count = 0;
            var users = new List<AcquireModel>();

            var cachedAcquireModel = (AcquireModel)HttpContext.Cache["acquireModel"];

            if (cachedAcquireModel == null)
                return Json(new { status = "failed" });

            users = cachedAcquireModel.Acquire.Skip((index - 1) * Setting.Page_Size).Take(Setting.Page_Size).ToList<AcquireModel>();
            count = cachedAcquireModel.Acquire.Count;

            return Json(new { status = "success", usersList = users, currentIndex = index, recordsCount = count }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Search Users in the Audit 
        /// </summary>
        [HttpGet]
        public JsonResult AcuireSearch(string name, string logicalid)
        {

            var users = new List<AcquireModel>();
            var count = 0;
            int logic = logicalid == null ? 0 : Convert.ToInt16(logicalid);
            var usersQuery = acquireTask.Search(name == null ? string.Empty : name, logic);

            foreach (var user in usersQuery)
            {
                users.Add(new AcquireModel()
                {
                     Id = user.Id,
                    name = user.name,
                    logicalid = user.LogicalId,
                    Enabled = user.Enabled                  
                });
            }

            HttpContext.Cache["acquireModel"] = new AcquireModel() { name = name, logicalid = Convert.ToInt16(logicalid) };
            count = users.Count;

            return Json(new { status = "success", userList = users.Take(Setting.Page_Size).ToList<AcquireModel>(), recordsCount = count }
                , JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult AcquireInsert()
        {
            //if (Request.Form.GetValues("txtName") == null)
            //{
            //    TempData["errorUsername"] = "System error. Please try again later";
            //    return Redirect("txtName?action=postusername");
            //}
            //if (Request.Form.GetValues("txtlogicalid") == null)
            //{
            //    TempData["errorUsername"] = "System error. Please try again later";
            //    return Redirect("txtName?action=postusername");
            //}
            //if (Request.Form.GetValues("chkenable") == null)
            //{
            //    TempData["errorUsername"] = "System error. Please try again later";
            //    return Redirect("txtName?action=postusername");
            //}

            string name = Request.Form.GetValues("txtName")[0];
            int logicalid = Convert.ToInt32(Request.Form.GetValues("txtlogicalid")[0]);
             bool enable =false;
             if (Request.Form.GetValues("chkenable") == null)
                 enable = false;
             else if (Request.Form.GetValues("chkenable")[0] == "on")
                 enable = true;
         
            DateTime createdDate = DateTime.Now;
            string createdBy = "3f5d9e9b-8cc9-44fd-98b4-977355173d4b";

            var user = acquireTask.Create(logicalid, enable, name, createdDate, createdBy, createdDate, createdBy);

            if (user == null)
            {
                TempData["errorUsername"] = "Can not retireve user information";
                return Redirect("Acquire?action=Acquire");
            }

            TempData["error"] = "Data saved";
            return Redirect("Acquire");
            //return Redirect("Acquire?action=Acquire");
        }
        public ActionResult AcquireUpdate()
        {
            //if (Request.Form.GetValues("txtName") == null)
            //{
            //    TempData["errorUsername"] = "System error. Please try again later";
            //    return Redirect("txtName?action=postusername");
            //}
            //if (Request.Form.GetValues("txtlogicalid") == null)
            //{
            //    TempData["errorUsername"] = "System error. Please try again later";
            //    return Redirect("txtName?action=postusername");
            //}
            //if (Request.Form.GetValues("chkenable") == null)
            //{
            //    TempData["errorUsername"] = "System error. Please try again later";
            //    return Redirect("txtName?action=postusername");
            //}

            string name = Request.Form.GetValues("txtName")[0];
            int logicalid = Convert.ToInt32(Request.Form.GetValues("txtlogicalid")[0]);
            bool enable = false;
            if (Request.Form.GetValues("chkenable") == null)
                enable = false;
            else if (Request.Form.GetValues("chkenable")[0] == "on")
                enable = true;

            DateTime createdDate = DateTime.Now;
            string createdBy = "3f5d9e9b-8cc9-44fd-98b4-977355173d4b";

            var user = acquireTask.Create(logicalid, enable, name, createdDate, createdBy, createdDate, createdBy);

            if (user == null)
            {
                TempData["errorUsername"] = "Can not retireve user information";
                return Redirect("Acquire?action=Acquire");
            }

            TempData["error"] = "Data saved";
            return Redirect("Acquire");
            //return Redirect("Acquire?action=Acquire");
        }
        /// <summary>
        /// Fetch data from the cached Acquie list in the page index change
        /// </summary>
        //[HttpGet]
        //public JsonResult GetAcqurie(string acquireid)
        //{
        //    //var count = 0;
        //    //var logs = new List<LogModel>();

        //    //var cachedLogs = (List<LogModel>)HttpContext.Cache["Logs"];

        //    //if (cachedLogs == null)
        //    //    return Json(new { status = "failed" });

        //    //logs = cachedLogs.Skip((index - 1) * Setting.Page_Size).Take(Setting.Page_Size).ToList<LogModel>();
        //    //count = cachedLogs.Count; 
        //    var acquireModel = new AcquireModel() {  name = string.Empty, logicalid = 0,Enabled=false };
        //    //var user = acquireTask.UpdateAcquire(acquireModel);


        //    //return Json(new { status = "success", name = user.name, logicalid = user.LogicalId, enable = user.Enabled }, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Fetch data from the cached Acquie list in the page index change
        /// </summary>
        [HttpGet]
        public JsonResult GetAcqurie(string acquireid)
        {
            var user = acquireTask.GetAcquiredata(Convert.ToInt32(acquireid));

            return Json(new { status = "success", name = user.name, logicalid = user.LogicalId, enable = user.Enabled }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// delete data from the  Acquire list 
        /// </summary>
        [HttpGet]
        public JsonResult delAcqurie(string acquireid)
        {
            var user = acquireTask.Delete(Convert.ToInt32(acquireid));
            //var count = 0;
            //var users = new List<AcquireModel>();         
            //var cachedAuditModel = (AcquireModel)HttpContext.Cache["acquireModel"];

            //if (cachedAuditModel == null)
            //    return Json(new { status = "failed" });

            //users = users.Take(Setting.Page_Size).ToList<AcquireModel>();
            //count = cachedAuditModel.Acquire.Count;

            var count = 0;
            var users = new List<AcquireModel>();

            var cachedAcquireModel = (AcquireModel)HttpContext.Cache["acquireModel"];

            if (cachedAcquireModel == null)
                return Json(new { status = "failed" });

            var usersQuery = acquireTask.GetAll();

            foreach (var acquire in usersQuery)
            {
                users.Add(new AcquireModel()
                {
                    Id = acquire.Id,
                    name = acquire.name,
                    logicalid = acquire.LogicalId,
                    Enabled = acquire.Enabled
                });
            }
            count = users.Count;
            users = users.Take(Setting.Page_Size).ToList<AcquireModel>();
          
            return Json(new { status = "success", usersList = users, recordsCount = count }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
