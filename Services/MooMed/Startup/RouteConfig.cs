using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace MooMed.Web.Startup
{
    public class RouteConfig
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

        public static void RegisterRoutes([NotNull] IRouteBuilder routeBuilder)
        {
            // Route attempts on reload of LogonPage with url edited by react router back to original logon
            foreach (var subRoute in RouteDirectory.LogonRoutes)
            {
                routeBuilder.MapRoute(
                    name: $"{subRoute}",
                    template: $"{subRoute}",
                    defaults: new { controller = "Home", action = "Index", route = subRoute }
                );
            }

            // Route attempts on reload of Mainpage with url edited by react router back to original logon
            foreach (var subRoute in RouteDirectory.MainRoutes)
            {
                routeBuilder.MapRoute(
                    name: $"{subRoute}",
                    template: $"{subRoute}",
                    defaults: new { controller = "Home", action = "Index", route = subRoute }
                );
            }

            routeBuilder.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        }

        public static void ConfigureRoute([NotNull] IApplicationBuilder app)
        {
            app.UseMvc(RegisterRoutes);
        }
    }
}
