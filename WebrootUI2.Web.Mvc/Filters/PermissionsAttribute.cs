using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebrootUI2.Domain;
using WebrootUI2.Web.Mvc.Providers;

namespace WebrootUI2.Web.Mvc.Filters
{
    public class PermissionsAttribute : ActionFilterAttribute
    {
        public Common.AdminPermission [] AllowedPermissions;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || !SecurityProvider.HasPermission(filterContext.HttpContext.User.Identity.Name, AllowedPermissions))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"Controller","App"},
                    {"Action","PermissionDeniedError"}
                });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}