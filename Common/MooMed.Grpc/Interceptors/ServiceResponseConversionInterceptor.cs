using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Hosting;
using MooMed.Core.DataTypes;
using MooMed.DotNet.Utils.Environment;

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
            catch (Exception)
            {
                if (EnvHelper.GetDeployment().Equals(Environments.Development))
                {
                    throw;
                }

                var responseType = typeof(TResponse);
                if (responseType.BaseType != ServiceResponseType)
                {
                    throw;
                }

                var defaultResponse = Activator.CreateInstance<TResponse>();
                return defaultResponse;
            }
        }
    }
}