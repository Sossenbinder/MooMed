using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace MooMed.Frontend.StartupConfigs
{
    public static class RouteConfig
    {
        public static void RegisterRoutes([NotNull] IEndpointRouteBuilder endpoints)
        {
	        endpoints.MapControllerRoute(
                "default",
	            "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapFallbackToController("Index", "Home");
        }
    }
}
