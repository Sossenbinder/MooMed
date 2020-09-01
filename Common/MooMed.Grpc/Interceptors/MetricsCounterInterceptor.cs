using System.Threading.Tasks;
using App.Metrics;
using Grpc.Core;
using Grpc.Core.Interceptors;
using MooMed.Module.Monitoring.Service.Interface;

namespace MooMed.Grpc.Interceptors
{
	public class MetricsCounterInterceptor : Interceptor
	{
		private readonly IMetrics _metrics;

		private readonly IGrpcMetricsService _grpcMetricsService;

		public MetricsCounterInterceptor(
			IMetrics metrics,
			IGrpcMetricsService grpcMetricsService)
		{
			_metrics = metrics;
			_grpcMetricsService = grpcMetricsService;
		}

		public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
			TRequest request,
			ServerCallContext context,
			UnaryServerMethod<TRequest, TResponse> interceptedCall)
		{
			using (_metrics.Measure.Timer.Time(_grpcMetricsService.GrpcCallTimer))
			{
				var response = await interceptedCall(request, context);

				return response;
			}
		}
	}
}