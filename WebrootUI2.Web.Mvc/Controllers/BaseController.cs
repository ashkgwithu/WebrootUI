using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using WebrootUI2.Domain;
using WebrootUI2.Domain.Contracts.Tasks;
using WebrootUI2.Web.Mvc.Controllers.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace WebrootUI2.Web.Mvc.Controllers
{
    public class BaseController : Controller
    {

        public BaseController()
        {
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Check for the language cookie, if availabe replace current language else set to default
            var cultureName = string.Empty;

            HttpCookie cultureCookie = Request.Cookies["_culture"];

            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages[0];

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            base.OnActionExecuted(filterContext);
        }
    }
}
