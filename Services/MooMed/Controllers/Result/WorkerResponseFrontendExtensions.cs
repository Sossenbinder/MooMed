using JetBrains.Annotations;
using MooMed.Core.DataTypes;

namespace MooMed.Web.Controllers.Result
{
    public static class WorkerResponseFrontendExtensions
    {
        [NotNull]
        public static JsonResponse ToJsonResponse<TPayload>([NotNull] this WorkerResponse<TPayload> workerResponse)
		{
			var workerResponseObj = new WorkerResponseFrontendContainer<TPayload>
			{
				Data = workerResponse.PayloadOrNull,
				ErrorMessage = workerResponse.ErrorMessage?.Message
			};

			return workerResponse.IsSuccess ? JsonResponse.Success(workerResponseObj) : JsonResponse.Error(workerResponseObj);
		}
    }
}