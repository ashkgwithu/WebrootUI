namespace WebrootUI2.Web.Mvc.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes) 
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "HttpNotFoundError",
                "HttpNotFoundError/{action}/{id}",
                new { controller = "App", action = "HttpNotFoundError", id = UrlParameter.Optional });

            routes.MapRoute(
                "PermissionDeniedError",
                "PermissionDeniedError",
                new { controller = "App", action = "PermissionDeniedError" });

            routes.MapRoute(
                "Companies",
                "Companies/{action}",
                new { controller = "Company", action = "Index" },
                new { action = "(Index|List|New)" });

            routes.MapRoute(
                "Roles",
                "Roles/{action}",
                new { controller = "Role", action = "Index" },
                new { action = "(Index|List|New)" });

            routes.MapRoute(
                "Users",
                "Users/{action}/{roleId}",
                new { controller = "User", action = "Index", roleId = System.Guid.Empty.ToString() },
                new { action = "(Index|List|New)" });

            routes.MapRoute(
                "UsersByCompany",
                "User/GetByCompany/{companyId}/{userId}",
                new { controller = "User", action = "GetByCompany" });

            //routes.MapRoute(
            //    "Default",
            //    "{controller}/{action}/{id}",
            //    new { controller = "App", action = "HttpNotFoundError", id = UrlParameter.Optional });
            routes.MapRoute(
               "Default",                                              // Route name
               "{controller}/{action}/{id}",                           // URL with parameters
               new { controller = "Admin", action = "LoginAction", id = "" } 
           );
        }
    }
}