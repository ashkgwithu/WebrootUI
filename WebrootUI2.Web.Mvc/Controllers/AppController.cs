using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebrootUI2.Web.Mvc.Controllers
{
    public class AppController : BaseController
    {
        public ActionResult ChangeLang(string lang)
        {
            if (lang != null && lang != string.Empty)
            {
                HttpCookie cookie = Request.Cookies["_culture"];

                if (cookie != null)
                    cookie.Value = lang;
                else
                {
                    cookie = new HttpCookie("_culture");
                    cookie.HttpOnly = false;
                    cookie.Value = lang;
                    cookie.Expires = DateTime.Now.AddYears(1);
                }
                Response.Cookies.Add(cookie); 
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            else
                return Redirect("/");
        }

        public ActionResult Help(string path)
        {
            string filename = path.Replace("/", "_") + ".html";
            HttpCookie cultureCookie = Request.Cookies["_culture"];

            string culture;
            if (cultureCookie != null)
                culture = cultureCookie.Value;
            else
                culture = Request.UserLanguages[0];
            
            if (string.IsNullOrEmpty(culture))
                culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

            string fullPath = "~/Views/Help/" + culture + "/" + filename;
            return new FilePathResult(fullPath, "text/html");
        }

        public ActionResult HttpNotFoundError()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult PermissionDeniedError()
        {
            return View();
        }
    }
}
