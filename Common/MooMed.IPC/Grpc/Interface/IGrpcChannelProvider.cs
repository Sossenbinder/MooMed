using System.Threading.Tasks;
using Grpc.Net.Client;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.Grpc.Interface
{
	public interface IGrpcChannelProvider
	{
		/// <summary>
		/// Get a grpc channel for a service, given an optional replica number
		/// </summary>
		/// <param name="moomedService">Type of service</param>
		/// <param name="replicaNumber">Number of replica to target</param>
		/// <returns>Grpc channel to create clients from</returns>
		GrpcChannel GetGrpcChannel(MooMedService moomedService, int replicaNumber = 0);
	}
}
