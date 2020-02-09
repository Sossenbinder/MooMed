using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace MooMed.Web.Startup
{
    public static class RouteConfig
    {
        private static class RouteDirectory
        {
            public static IEnumerable<string> LogonRoutes { get; } = new List<string>()
            {
                "login",
                "register",
                "forgotPassword",
            };

            public static IEnumerable<string> MainRoutes { get; } = new List<string>()
            {
                "editProfile",
                "about",
                "contact"
            };
        }

        public static void RegisterRoutes([NotNull] IEndpointRouteBuilder endpoints)
        {
            // Route attempts on reload of LogonPage with url edited by react router back to original logon
            foreach (var subRoute in RouteDirectory.LogonRoutes)
            {
                endpoints.MapControllerRoute(
                    subRoute,
                    subRoute,
                    new { controller = "Home", action = "Index", route = subRoute }
                );
            }

            // Route attempts on reload of Mainpage with url edited by react router back to original logon
            foreach (var subRoute in RouteDirectory.MainRoutes)
            {
                endpoints.MapControllerRoute(
                    subRoute,
                    subRoute,
                    new { controller = "Home", action = "Index", route = subRoute }
                );
            }

            endpoints.MapControllerRoute(
                "default",
	            "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
