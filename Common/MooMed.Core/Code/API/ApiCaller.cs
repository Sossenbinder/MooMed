using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.Code.API.Interface;
using Newtonsoft.Json;

namespace MooMed.Core.Code.API
{
    public class ApiCaller : IApiCaller
    {
        [NotNull]
        private readonly ApiWebRequestProvider m_apiWebRequestProvider;

        public ApiCaller([NotNull] ApiInformation apiInformation)
        {
            m_apiWebRequestProvider = new ApiWebRequestProvider(apiInformation);
        }

        [ItemNotNull]
        public async Task<ApiGetRequestResponse<TPayload>> SendGetRequest<TPayload>(string subRoute)
            where TPayload : class
        {
            var requestObject = m_apiWebRequestProvider.GenerateGetRequest(subRoute);

            var response = await requestObject.GetResponseAsync();

            var responseStream = response?.GetResponseStream();

            if (responseStream != null)
            {
                var responseString = await new StreamReader(responseStream).ReadToEndAsync();

                var objectifiedResponse = JsonConvert.DeserializeObject<TPayload>(responseString);

                return new ApiGetRequestResponse<TPayload>(ApiRequestResult.Success, objectifiedResponse);
            }

            return new ApiGetRequestResponse<TPayload>(ApiRequestResult.EndpointNotFound, null);
        }
    }
}
