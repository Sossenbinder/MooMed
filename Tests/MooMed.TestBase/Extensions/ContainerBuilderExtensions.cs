using System;
using Autofac;
using Autofac.Extras.Moq;
using MooMed.TestBase.DependencyInjection;

namespace MooMed.TestBase.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddAutoMoqSupport(
            this ContainerBuilder containerBuilder,
            Action<AutoMock>? moqProviderSetup = null,
            MockBehaviour mockBehaviour = MockBehaviour.Loose)
        {
            containerBuilder.RegisterSource(new AutoMoqRegistrationSource(new AutoMoqProvider(moqProviderSetup, mockBehaviour)));

            return containerBuilder;
        }
    }
}