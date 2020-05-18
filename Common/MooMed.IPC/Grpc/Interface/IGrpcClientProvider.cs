using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.IPC.Grpc.Interface
{
	/// <summary>
	/// Provides grpc clients for combinations of services and channels
	/// </summary>
	public interface IGrpcClientProvider
    {
		/// <summary>
		/// Provides a grpc setService for a given service and possible replica number 
		/// </summary>
		/// <typeparam name="TService">Type of GrpcService</typeparam>
		/// <param name="moomedService">Target service</param>
		/// <param name="replicaNumber">optional - Number of replica to target</param>
		/// <returns>Object of TService to communicate through</returns>
		[NotNull]
        TService GetGrpcClient<TService>(MooMedService moomedService, int replicaNumber = 0)
	        where TService : class, IGrpcService;
    }
}
