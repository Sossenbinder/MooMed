using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using MooMed.Core.DataTypes;

namespace MooMed.Grpc.Interceptors
{
	public class ErrorHandlingInterceptor : Interceptor
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
				var responseType = typeof(TResponse);
				if (responseType.BaseType == ServiceResponseType)
				{
					var defaultResponse = Activator.CreateInstance<TResponse>();
					return defaultResponse;
				}

				throw;
			}
		}
	}
}