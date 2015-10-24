namespace Codex.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Hard-coded Routes
            routes.MapRoute("Home-CatchAll",            "{controller}/{action}",        new { controller = "Home", action = "Index" });
            routes.MapRoute("Home-Index",               "Home/Index",                   new { controller = "Home", action = "Index" });

            routes.MapRoute("Account-Register",         "Account/Register",             new { controller = "Account", action = "Register" });
            routes.MapRoute("Account-Login",            "Account/Login",                new { controller = "Account", action = "Login" });
            routes.MapRoute("Account-ForgotPassword",   "Account/ForgotPassword",       new { controller = "Account", action = "ForgotPassword" });
            routes.MapRoute("Account-Settings",         "Account/Settings",             new { controller = "Account", action = "Settings" });
            routes.MapRoute("Account-Logout",           "Account/Logout",               new { controller = "Account", action = "Logout" });
            
            routes.MapRoute("Article-Create",           "Article/Create",               new { controller = "Article", action = "Create" });
            routes.MapRoute("Article-Edit",             "Article/{id}/Edit",            new { controller = "Article", action = "Edit", id = UrlParameter.Optional });
            routes.MapRoute("Article-View",             "Article/{id}",                 new { controller = "Article", action = "View", id = UrlParameter.Optional });
            
            // routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
