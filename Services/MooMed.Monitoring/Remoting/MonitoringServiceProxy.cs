using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

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