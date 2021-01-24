using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac.Core.Activators.ProvidedInstance;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using MooMed.TestBase.DependencyInjection.Interface;

namespace MooMed.TestBase.DependencyInjection
{
	public class AutoMoqRegistrationSource : IRegistrationSource
	{
		private readonly List<string> _internalTypesDescription;

		private readonly IAutoMoqProvider _autoMoqProvider;

		public AutoMoqRegistrationSource(IAutoMoqProvider autoMoqProvider)
		{
			_internalTypesDescription = new List<string>
			{
				"Autofac.IStartable",
				"AutoActivate",
				"Autofac.Builder.BuildCallbackService"
			};

			_autoMoqProvider = autoMoqProvider;
		}

		public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
		{
			// Block internal types
			if (_internalTypesDescription.Contains(service.Description))
			{
				return Enumerable.Empty<IComponentRegistration>();
			}

			var registrations = registrationAccessor(service);

			if (registrations.Any())
			{
				return Enumerable.Empty<IComponentRegistration>();
			}

			// We don't have any registration - Let's create a Mock
			var mock = _autoMoqProvider.CreateMock(service);

			if (mock == null)
			{
				return Enumerable.Empty<IComponentRegistration>();
			}

			return new List<IComponentRegistration>()
			{
				new ComponentRegistration(
					Guid.NewGuid(),
					new ProvidedInstanceActivator(mock!),
					new CurrentScopeLifetime(),
					InstanceSharing.Shared,
					InstanceOwnership.OwnedByLifetimeScope,
					new[] {service},
					new Dictionary<string, object?>())
			};
		}

		public bool IsAdapterForIndividualComponents { get; } = false;
	}
}