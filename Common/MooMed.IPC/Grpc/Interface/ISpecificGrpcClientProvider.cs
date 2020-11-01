using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.IPC.Grpc.Interface
{
	public interface ISpecificGrpcClientProvider
	{
		/// <summary>
		/// Provides a grpc setService for a given service and possible replica number
		/// </summary>
		/// <typeparam name="TService">Type of GrpcService</typeparam>
		/// <param name="moomedService">Target service</param>
		/// <param name="replicaNumber">optional - Number of replica to target</param>
		/// <returns>Object of TService to communicate through</returns>
		ValueTask<TService> GetGrpcClient<TService>(StatefulSetService moomedService, int replicaNumber = 0)
			where TService : class, IGrpcService;
	}
}