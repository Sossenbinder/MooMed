using System;
using System.Reflection;
using MooMed.Identity.Service.Identity.Interface;
using MooMed.Identity.Service.Interface;

namespace MooMed.Identity.Service.Identity
{
	internal class ServiceIdentityProvider : IServiceIdentityProvider
	{
		private readonly Lazy<string> _serviceIdentityLazy;

		public ServiceIdentityProvider(IDnsResolutionService dnsResolutionService)
		{
			_serviceIdentityLazy = new Lazy<string>(
				() => Assembly.GetEntryAssembly()?.GetName().Name ?? dnsResolutionService.GetOwnHostName());
		}

		public string GetServiceIdentity() => _serviceIdentityLazy.Value;
	}
}
