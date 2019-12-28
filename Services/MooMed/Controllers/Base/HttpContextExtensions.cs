using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace MooMed.Web.Controllers.Base
{
    public static class HttpContextExtensions
    {
        public static bool IsAuthenticated([NotNull] this HttpContext httpContext) =>
            httpContext.User?.Identity?.IsAuthenticated ?? false;
    }
}
