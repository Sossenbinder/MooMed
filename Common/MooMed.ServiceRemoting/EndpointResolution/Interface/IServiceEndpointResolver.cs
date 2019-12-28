using System.Threading.Tasks;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.EndpointResolution.Interface
{
    public interface IServiceEndpointResolver
    {
        ServiceDetail GetServiceDetail(DeployedFabricService deployedFabricService);

        Task<ServiceDetail> GetServiceDetailAsync(DeployedFabricService deployedFabricService);

        Task RefreshEndpointList();
    }
}
