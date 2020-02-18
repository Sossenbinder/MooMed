using JetBrains.Annotations;
using MooMed.Core.DataTypes;

namespace MooMed.Web.Controllers.Result
{
    public static class ServiceResponseFrontendExtensions
    {
        [NotNull]
        public static JsonResponse ToJsonResponse<TPayload>([NotNull] this ServiceResponse<TPayload> serviceResponse)
		{
			var serviceResponseObj = new ServiceResponseFrontendContainer<TPayload>
			{
				Success = serviceResponse.IsSuccess,
				Data = serviceResponse.PayloadOrNull,
				ErrorMessage = serviceResponse.ErrorMessage?.Message
			};

			return JsonResponse.Success(serviceResponseObj);
		}
    }
}