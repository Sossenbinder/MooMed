using System.IO;
using JetBrains.Annotations;

namespace MooMed.Core.Code.API
{
    public class ApiUrlGenerator
    {
        [NotNull]
        private readonly string m_baseUrl;

        public ApiUrlGenerator([NotNull] string baseUrl)
        {
            m_baseUrl = baseUrl;
        }

        [NotNull]
        public string CreateRequestUrl([NotNull] string subRoute)
        {
            return Path.Combine(m_baseUrl, subRoute);
        }
    }
}
