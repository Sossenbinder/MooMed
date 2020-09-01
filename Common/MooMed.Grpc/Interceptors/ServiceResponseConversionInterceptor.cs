using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MooMed.Core.DataTypes;

namespace MooMed.Grpc.Interceptors
{
	public class ServiceResponseConversionInterceptor : Interceptor
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		private static readonly Type ServiceResponseType = typeof(ServiceResponseBase);

		public ServiceResponseConversionInterceptor(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

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
				if (_webHostEnvironment.IsEnvironment(Environments.Development))
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