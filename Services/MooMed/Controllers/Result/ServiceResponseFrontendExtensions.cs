using JetBrains.Annotations;
using MooMed.Core.DataTypes;

namespace MooMed.Web.Controllers.Result
{
    public static class ServiceResponseFrontendExtensions
    {
	    [NotNull]
	    public static JsonResponse ToJsonResponse([NotNull] this ServiceResponse serviceResponse)
		    => JsonResponse.Success(serviceResponse.IsSuccess);

		[NotNull]
	    public static JsonResponse ToJsonResponse<TPayload>([NotNull] this ServiceResponse<TPayload> serviceResponse)
		    => JsonResponse.Success(serviceResponse.PayloadOrNull, serviceResponse.IsSuccess);
    }
}