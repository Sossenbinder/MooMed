using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac.Core;
using Autofac.Extras.Moq;
using MooMed.TestBase.DependencyInjection.Interface;

namespace MooMed.TestBase.DependencyInjection
{
	public class AutoMoqProvider : IAutoMoqProvider
	{
		private readonly AutoMock _autoMockProvider;

		private readonly List<Assembly> _assemblies;

		private readonly MethodInfo _genericMockCreator;

		public AutoMoqProvider(
			Action<AutoMock>? autoMoqConfigAction = null,
			MockBehaviour mockBehaviour = MockBehaviour.Loose)
		{
			_autoMockProvider = mockBehaviour == MockBehaviour.Loose ? AutoMock.GetLoose() : AutoMock.GetStrict();

			autoMoqConfigAction?.Invoke(_autoMockProvider);

			_assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

			_genericMockCreator = GetType().GetMethod(nameof(CreateGenericMock), BindingFlags.NonPublic | BindingFlags.Instance)!;
		}

		public object? CreateMock(Service service)
		{
			Type? serviceType = null;

			foreach (var assembly in _assemblies)
			{
				serviceType = assembly.GetType(service.Description);

				if (serviceType != null)
				{
					break;
				}
			}

			if (serviceType == null)
			{
				return null;
			}

			var genericCreate = _genericMockCreator.MakeGenericMethod(serviceType);

			return genericCreate == null ? null : genericCreate.Invoke(this, null);
		}

		private object? CreateGenericMock<T>()
		{
			return _autoMockProvider.Create<T>();
		}

		public void Dispose()
		{
			_autoMockProvider.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}