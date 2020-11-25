using System;
using System.Linq;
using System.Reflection;
using GrpcProxyGenerator.Extensions;
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
#if RELEASE
			var solutionPath = args[0];
#else
            var solutionPath = @"P:\Coding\Projects\Programmieren\C-Sharp\MooMed";
#endif

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IGrpcProxyEmitter, GrpcProxyEmitter>()
                .AddSingleton<IGrpcProxyFactory, GrpcProxyFactory>()
                .AddSingleton<ITypeInfoProvider, TypeInfoProvider>()
                .AddSingleton<string>(solutionPath)
                .BuildServiceProvider();

            var allGrpcServices = Assembly.GetAssembly(typeof(IGrpcService))
                ?.GetTypes()
                .Where(x => x.HasInterface(typeof(IGrpcService)))
                .OrderBy(x => x.FullName)
                .ToList();

            var factoryService = serviceProvider.GetServiceOrFail<IGrpcProxyFactory>();

            foreach (var service in allGrpcServices!)
            {
                factoryService.GenerateProxy(service);
            }
        }
    }
}