using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace MooMed.Web.Controllers.Result
{
    public class JsonResponse : JsonResult
    {
        private readonly HttpStatusCode m_statusCode;

        private JsonResponse([NotNull] object data, bool success, HttpStatusCode statusCode)
            :base(new
            {
                success,
                data
            })
        {
            m_statusCode = statusCode;
        }

        [NotNull]
        public static JsonResponse Success([CanBeNull] object data = null, bool internalSuccess = true)
        {
            return new JsonResponse(data ?? new EmptyResult(), internalSuccess, HttpStatusCode.OK);
        }

        [NotNull]
        public static JsonResponse Error([CanBeNull] object data = null)
        {
            return new JsonResponse(data ?? new EmptyResult(), false, HttpStatusCode.BadRequest);
        }

        public override Task ExecuteResultAsync([NotNull] ActionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)m_statusCode;
			return base.ExecuteResultAsync(context);
        }
    }
}