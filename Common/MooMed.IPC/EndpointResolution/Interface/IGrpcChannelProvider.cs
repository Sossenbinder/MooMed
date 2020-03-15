using System.Threading.Tasks;
using Grpc.Net.Client;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.EndpointResolution.Interface
{
	/// <summary>
	/// Provides grpc channels for combinations of services and channels
	/// </summary>
	public interface IGrpcChannelProvider
	{
		Task<GrpcChannel> GetGrpcChannelForService(StatefulSet statefulSet, int replicaNumber);
	}
}
