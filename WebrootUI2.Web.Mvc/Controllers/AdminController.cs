using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebrootUI2.Domain.Models;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Infrastructure.Common.Log;
using WebrootUI2.Resources;
using WebrootUI2.Web.Mvc.Controllers.Queries;
using WebrootUI2.Tasks.MCP;

namespace WebrootUI2.Web.Mvc.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly IEmailTask emailTask;
        private readonly IVerificationTokenTask verTokenTask;
        private readonly UserQuery userQuery;

        public AdminController(IEmailTask emailTask, IVerificationTokenTask verTokenTask, UserQuery userQuery)
        {
            this.emailTask = emailTask;
            this.verTokenTask = verTokenTask;
            this.userQuery = userQuery;
        }

        #region Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "postanswer")
            {
                if (Session["username"] == null)
                {
                    TempData["errorAnswer"] = "System error. Please try again later";
                    return View();
                }

                var user = Membership.GetUser(Session["username"].ToString());

                if (user == null)
                {
                    TempData["errorAnswer"] = "Can not retireve user information";
                    return View();
                }

                if (string.IsNullOrEmpty(user.PasswordQuestion))
                {
                    TempData["errorAnswer"] = "Password question is not provided";
                    return View();
                }

                ViewData["question"] = user.PasswordQuestion;
                return View();
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginAction()
        {

            if (Request.Form.GetValues(CommonResources.User_Username.ToLower()) == null ||
                Request.Form.GetValues(CommonResources.User_Password.ToLower()) == null)
            {
                TempData["error"] = "System error. Please try again later";
                return RedirectToAction("Login");
            }

            var username = Request.Form.GetValues(CommonResources.User_Username.ToLower())[0];
            var password = Request.Form.GetValues(CommonResources.User_Password.ToLower())[0];

            var user = Membership.GetUser(username);

            if (user != null && user.IsLockedOut)
            {
                TempData["error"] = "User account is locked-out";
                LogManager.Log("Login failed due to account lock-out", LogType.info, (Guid)user.ProviderUserKey);

                return RedirectToAction("Login");
            }

            if (!Membership.ValidateUser(username, password))
            {
                TempData["error"] = "Invalid Username or Password";
                //LogManager.Log("Login failed due to invalid username or password", LogType.error, username);

                var failedUser = Membership.GetUser(username);

                if (failedUser != null && failedUser.IsLockedOut)
                {
                    TempData["error"] = "User account has been locked-out";
                    LogManager.Log("User account has been locked-out", LogType.info, (Guid)failedUser.ProviderUserKey);
                }

                return RedirectToAction("Login");
            }

            Guid adminId = userQuery.GetAdministratorId((Guid)user.ProviderUserKey);

            if (adminId != Guid.Empty)
                Setting.AdministratorId = adminId;
            else
            {
                TempData["error"] = "You do not have permission.";
                LogManager.Log("Login failed due to permission constraint", LogType.error, (Guid)user.ProviderUserKey);
                return RedirectToAction("Login");
            }            

            FormsAuthentication.SetAuthCookie(username, true);
            LogManager.Log("Successful loged-in", LogType.info, (Guid)user.ProviderUserKey);
            return RedirectToAction("Home");
        }

        public ActionResult SignOut()
        {
            Setting.AdministratorId = Guid.Empty;
            FormsAuthentication.SignOut();
            LogManager.Log("Successful loged-out", LogType.info, (Guid)Membership.GetUser().ProviderUserKey);
            return RedirectToAction("Login");
        }

        #endregion

        public ActionResult Home()
        {
            return View();
        }

        #region Reset password

        [AllowAnonymous]
        public ActionResult PostUsername()
        {
            if (Request.Form.GetValues("forgotUsername") == null)
            {
                TempData["errorUsername"] = "System error. Please try again later";
                return Redirect("Login?action=postusername");
            }

            var username = Request.Form.GetValues("forgotUsername")[0];
            var user = Membership.GetUser(username);

            if (user == null)
            {
                TempData["errorUsername"] = "Can not retireve user information";
                return Redirect("Login?action=postusername");
            }

            Session["username"] = username;
            return Redirect("Login?action=postanswer");
        }

        [AllowAnonymous]
        public ActionResult PostAnswer()
        {
            if (Request.Form.GetValues("answer") == null)
            {
                TempData["errorAnswer"] = "System error. Please try again later";
                return Redirect("Login?action=postanswer");
            }

            var answer = Request.Form.GetValues("answer")[0];
            var user = Membership.GetUser(Session["username"].ToString());

            if (user == null)
            {
                TempData["errorAnswer"] = "Can not retireve user information";
                return Redirect("Login?action=postanswer");
            }

            if (user.IsLockedOut)
            {
                TempData["errorAnswer"] = "User account is locked-out";
                return Redirect("Login?action=postanswer");
            }

            try
            {

                user.ResetPassword(answer);

                VerificationToken token = verTokenTask.Create(user.ProviderUserKey.ToString());

                if (token.Token.ToString() == string.Empty)
                {
                    TempData["errorAnswer"] = "System error. Please try again later";
                    return Redirect("Login?action=postanswer");
                }

                if (!emailTask.SendResetPWMail(user.UserName, user.Email, token.Token.ToString(), "Admin"))
                {
                    TempData["errorAnswer"] = "System error. Please try again later";
                    return Redirect("Login?action=postanswer");
                }
            }
            catch (MembershipPasswordException ex)
            {
                LogManager.Log("Invalid password answer attempt", LogType.error, (Guid)user.ProviderUserKey);
                var failedUser = Membership.GetUser(user.UserName);

                if (failedUser.IsLockedOut)
                {
                    TempData["errorAnswer"] = "Account has been locket-out";
                    LogManager.Log("Maximum invalid password answer Attempts exceeded.Account has been locket-out", LogType.info, (Guid)user.ProviderUserKey);
                }
                else
                    TempData["errorAnswer"] = ex.Message;

                return Redirect("Login?action=postanswer");
            }

            Session.Remove("username");
            return Redirect("Login?action=success");
        }

        [AllowAnonymous]
        public ActionResult VerifyResetPassword()
        {
            if (Request.QueryString["action"] == null)
            {
                if (Request.QueryString["token"] == null)
                {
                    TempData["error"] = "Invalid url. Please contact administrator.";
                    return View();
                }

                string userId = verTokenTask.IsValidToken(Request.QueryString["token"]);

                if (userId == string.Empty)
                {
                    TempData["error"] = "Token is invalid or expired.  Please contact administrator.";
                    return View();
                }

                Session["userid"] = userId;
                Session["token"] = Request.QueryString["token"];
                return Redirect("verifyResetpassword?action=changepassword");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            if (Request.Form.GetValues("password") == null || Session["userId"] == null)
            {
                TempData["error"] = "System error. Please try again later";
                return Redirect("verifyResetpassword?action=changepassword");
            }

            string password = Request.Form.GetValues("password")[0];

            //Use different provider to support ChangePassword method.
            var user = Membership.Providers["AdminProvider"].GetUser(Guid.Parse(Session["userid"].ToString()), false);

            if (user == null)
            {
                TempData["error"] = "Can not retireve user information";
                return Redirect("verifyResetpassword?action=changepassword");
            }

            try
            {
                if (!user.ChangePassword(user.GetPassword(), password))
                {
                    TempData["error"] = "Can not change the password.";
                    return Redirect("verifyResetpassword?action=changepassword");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Redirect("verifyResetpassword?action=changepassword");
            }

            verTokenTask.Delete(Session["token"].ToString());

            Session.Remove("userid");
            Session.Remove("token");
            LogManager.Log("Successful password change", LogType.info, (Guid)user.ProviderUserKey);
            return Redirect("verifyResetpassword?action=success");
        }

        #endregion

        public ActionResult Accounts()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");
            return View();
        }

        public ActionResult Transactions()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Invoices()
        {
            return View();
        }

        public ActionResult System()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            return View("_Profile");
        }
    }
}