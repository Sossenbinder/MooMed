using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Core.Code.API.Interface
{
    public interface IApiCaller
    {
        Task<ApiGetRequestResponse<TPayload>> SendGetRequest<TPayload>([NotNull] string subRoute)
            where TPayload : class;
    }
}
