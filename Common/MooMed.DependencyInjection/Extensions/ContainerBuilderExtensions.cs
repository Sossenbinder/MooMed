using System.Diagnostics.CodeAnalysis;
using Autofac;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.DependencyInjection.Extensions
{
	public static class ContainerBuilderExtensions
	{
		public static void RegisterGrpcService<TServiceInterface, TServiceImpl>([NotNull] this ContainerBuilder builder)
			where TServiceImpl : TServiceInterface
			where TServiceInterface : IGrpcService
		{
			builder.RegisterType<TServiceImpl>()
				.As<TServiceInterface, IGrpcService>()
				.SingleInstance();
		}
	}
}