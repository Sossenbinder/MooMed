﻿using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.ServiceBase.Definitions.Interface;

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
		/// <returns>Object of TService to communicate through</returns>
		ValueTask<TService> GetGrpcClient<TService>(DeploymentService moomedService)
			where TService : class, IGrpcService;
	}
}