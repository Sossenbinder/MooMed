using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace MooMed.Frontend.Controllers.Result
{
	public class JsonResponse : JsonResult
	{
		private readonly HttpStatusCode _statusCode;

		private JsonResponse([CanBeNull] object data, bool success, HttpStatusCode statusCode)
			: base(new
			{
				success,
				data
			})
		{
			_statusCode = statusCode;
		}

		[NotNull]
		public static JsonResponse Success([CanBeNull] object data = null, bool internalSuccess = true)
		{
			return new JsonResponse(data, internalSuccess, HttpStatusCode.OK);
		}

		[NotNull]
		public static JsonResponse Error([CanBeNull] object data = null)
		{
			return new JsonResponse(data, false, HttpStatusCode.OK);
		}

		public override Task ExecuteResultAsync([NotNull] ActionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)_statusCode;
			return base.ExecuteResultAsync(context);
		}
	}
}