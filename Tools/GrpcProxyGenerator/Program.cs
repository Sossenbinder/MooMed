using System;
using System.Linq;
using System.Reflection;
using GrpcProxyGenerator.Service;
using GrpcProxyGenerator.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using MooMed.DotNet.Extensions;
using MooMed.ServiceBase.Definitions.Interface;
using MooMed.ServiceBase.Services.Interface;

namespace GrpcProxyGenerator
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			//setup our DI
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddSingleton<IProxyEmitService, ProxyEmitService>()
				.AddSingleton<IGrpcProxyFactory, GrpcProxyFactory>()
				.AddSingleton<ITypeInfoProvider, TypeInfoProvider>()
				.BuildServiceProvider();

			var allGrpcServices = Assembly.GetAssembly(typeof(IGrpcService))
				?.GetTypes()
				.Where(x => x.HasInterface(typeof(IGrpcService)))
				.OrderBy(x => x.FullName)
				.ToList();

			var factoryService = serviceProvider.GetService<IGrpcProxyFactory>();

			foreach (var service in allGrpcServices!)
			{
				factoryService.GenerateProxy(typeof(IProfilePictureService));
			}
		}
	}
}