using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Hosting;
using MooMed.Core.DataTypes;
using MooMed.DotNet.Utils.Environment;
using System;
using System.Threading.Tasks;

namespace MooMed.Grpc.Interceptors
{
    public class ServiceResponseConversionInterceptor : Interceptor
    {
        private static readonly Type ServiceResponseType = typeof(ServiceResponseBase);

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> interceptedCall)
        {
            try
            {
                return await interceptedCall(request, context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (EnvHelper.GetDeployment().Equals(Environments.Development))
                {
                    throw;
                }

                var responseType = typeof(TResponse);
                if (responseType.BaseType != ServiceResponseType)
                {
                    throw;
                }

                return (Activator.CreateInstance(responseType, true) as TResponse)!;
            }
        }
    }
}