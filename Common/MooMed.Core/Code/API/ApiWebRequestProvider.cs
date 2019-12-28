using System.Net;
using JetBrains.Annotations;

namespace MooMed.Core.Code.API
{
    public class ApiWebRequestProvider
    {
        [NotNull]
        private readonly ApiUrlGenerator m_apiUrlGenerator;

        public ApiWebRequestProvider([NotNull] ApiInformation apiInformation)
        {
            m_apiUrlGenerator = new ApiUrlGenerator(apiInformation.EndpointBaseUrl);
        }

        [NotNull]
        private HttpWebRequest GenerateBasicHttpWebRequest([NotNull] string subRoute)
        {
            var requestUrl = m_apiUrlGenerator.CreateRequestUrl(subRoute);

            var webRequest = WebRequest.CreateHttp(requestUrl);
            webRequest.ContentType = "application/x-www-form-urlencoded";

            return webRequest;
        }

        [NotNull]
        public HttpWebRequest GenerateGetRequest([NotNull] string subRoute)
        {
            var webRequest = GenerateBasicHttpWebRequest(subRoute);

            webRequest.Method = "GET";

            return webRequest;
        }

    }
}
