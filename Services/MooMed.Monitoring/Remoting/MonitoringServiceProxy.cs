﻿using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Grpc.Services.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;

namespace MooMed.Monitoring.Remoting
{
	public class MonitoringServiceProxy : AbstractDeploymentProxy<IMonitoringService>, IMonitoringService
	{
		public MonitoringServiceProxy([NotNull] IGrpcClientProvider clientProvider)
			: base(clientProvider, DeploymentService.MonitoringService)
		{
		}
	}
}