using System.Threading.Tasks;
using Grpc.Net.Client;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.Grpc.Interface
{
	public interface ISpecificGrpcChannelProvider
	{
		ValueTask<GrpcChannel> GetGrpcChannel(StatefulSetService moomedService, int replicaNumber = 0);
	}
}