using System.Linq;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace MooMed.AspNetCore.Extensions
{
	public static class WebHostBuilderExtensions
	{
		public static IWebHostBuilder ConfigureGrpc(this IWebHostBuilder webHostBuilder, int port = 10042)
		{
			webHostBuilder
				.UseKestrel(options =>
				{
					// Compatibility to allow regular calls for health checks
					options.ListenAnyIP(80);
					options.ListenAnyIP(443);

					options.ListenAnyIP(port, listenOptions =>
					{
						listenOptions.Protocols = HttpProtocols.Http2;
					});
				});

			return webHostBuilder;
		}

		public static IWebHostBuilder AddAppMetricsWithPrometheusSupport(this IWebHostBuilder webHostBuilder)
		{
			var metrics = AppMetrics.CreateDefaultBuilder()
				.OutputMetrics.AsPrometheusPlainText()
				.OutputMetrics.AsPrometheusProtobuf()
				.Build();

			return webHostBuilder
				.UseMetrics(options =>
				{
					options.EndpointOptions = endpointOptions =>
					{
						endpointOptions.MetricsTextEndpointOutputFormatter = metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
						endpointOptions.MetricsEndpointOutputFormatter = metrics.OutputMetricsFormatters.OfType<MetricsPrometheusProtobufOutputFormatter>().First();
					};
				})
				.UseMetricsEndpoints();
		}
	}
}