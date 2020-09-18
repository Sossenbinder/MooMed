using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace MooMed.Frontend.Controllers.Result
{
	public class JsonResponse : JsonResult
	{
		private readonly HttpStatusCode _statusCode;

		protected JsonResponse(object? data, bool success, HttpStatusCode statusCode)
			: base(new
			{
				success,
				data
			})
		{
			_statusCode = statusCode;
		}

		[NotNull]
		public static JsonResponse Success(object? data = null, bool internalSuccess = true)
		{
			return new JsonResponse(data, internalSuccess, HttpStatusCode.OK);
		}

		[NotNull]
		public static JsonDataResponse<TPayload> Success<TPayload>(TPayload data = default, bool internalSuccess = true)
		{
			return new JsonDataResponse<TPayload>(data, internalSuccess, HttpStatusCode.OK);
		}

		[NotNull]
		public static JsonResponse Error(object? data = null)
		{
			return new JsonResponse(data, false, HttpStatusCode.OK);
		}

		public override Task ExecuteResultAsync([NotNull] ActionContext context)
		{
			context.HttpContext.Response.StatusCode = (int)_statusCode;
			return base.ExecuteResultAsync(context);
		}
	}

	public class JsonDataResponse<T> : JsonResponse
	{
		public JsonDataResponse(T data, bool success, HttpStatusCode statusCode)
			: base(data, success, statusCode)
		{
		}

		[NotNull]
		public static JsonDataResponse<T> Success(T data = default, bool internalSuccess = true)
		{
			return new JsonDataResponse<T>(data, internalSuccess, HttpStatusCode.OK);
		}

		[NotNull]
		public static JsonDataResponse<T> Error(T data = default)
		{
			return new JsonDataResponse<T>(data, false, HttpStatusCode.OK);
		}
	}
}