using JetBrains.Annotations;

namespace MooMed.Core.Code.API
{
    public class ApiRequestResponse
    {
        public ApiRequestResult ApiRequestResult { get; private set; }

        public ApiRequestResponse(ApiRequestResult apiRequestResult)
        {
            ApiRequestResult = apiRequestResult;
        }
    }

    public class ApiGetRequestResponse<TPayload> : ApiRequestResponse
        where TPayload : class
    {
        public TPayload Payload { get; private set; }

        public ApiGetRequestResponse(ApiRequestResult apiRequestResult, [NotNull] TPayload payload)
            :base(apiRequestResult)
        {
            Payload = payload;
        }
    }
}
