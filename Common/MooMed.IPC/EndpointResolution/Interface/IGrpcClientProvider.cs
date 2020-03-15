using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.IPC.EndpointResolution.Interface
{
	/// <summary>
	/// Provides grpc clients for combinations of services and channels
	/// </summary>
	public interface IGrpcClientProvider
    {
		[ItemCanBeNull]
        Task<TService> GetGrpcClientAsync<TService>(StatefulSet statefulSet, int replicaNumber)
	        where TService : class, IGrpcService;
    }
}
