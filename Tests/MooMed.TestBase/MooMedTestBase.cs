using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;
using MooMed.Configuration.Module;
using MooMed.Core;
using MooMed.Encryption.Interface;
using MooMed.Encryption.Module;
using MooMed.Logging.Loggers.Helper.Interface;
using MooMed.Logging.Module;
using MooMed.TestBase.Config;
using MooMed.TestBase.Utils;
using NUnit.Framework;

namespace MooMed.TestBase
{
    public class MooMedTestBase
    {
        protected string TestDir { get; private set; } = string.Empty;

        protected IConfigProvider UnitTestProvider { get; private set; }

        private ContainerBuilder ContainerBuilder { get; set; }

        protected IContainer Container { get; set; }

        public MooMedTestBase()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            SetupFixture();
        }

        ~MooMedTestBase()
        {
            TearDownFixture();
        }

        protected virtual void SetupFixture()
        {
            TestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!);
        }

        [SetUp]
        public void SetupEntry()
        {
            ContainerBuilder = new ContainerBuilder();
            SetupContainer(ContainerBuilder);

            AddDefaultRegistrations();
            Container = ContainerBuilder.Build();
            UnitTestProvider = Container.Resolve<IConfigProvider>();

            Setup();
        }

        #region Nunit Events

        protected virtual void SetupContainer(ContainerBuilder builder)
        {
        }

        protected virtual void Setup()
        {
        }

        [TearDown]
        protected virtual void TearDown() { }

        protected virtual void TearDownFixture()
        {
        }

        #endregion Nunit Events

        private void AddDefaultRegistrations()
        {
            ContainerBuilder.RegisterModule<CoreModule>();
            ContainerBuilder.RegisterModule<EncryptionModule>();
            ContainerBuilder.RegisterModule<ConfigurationModule>();
            ContainerBuilder.RegisterModule<LoggingModule>();
            ContainerBuilder.RegisterModule<ConfigurationModule>();

            var config = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource()
                {
                    Path = "UnitTestSettings.json"
                })
                .Build();

            ContainerBuilder
                .Register(x => new Configuration.Config(config))
                .As<IConfig>()
                .SingleInstance();

            ContainerBuilder.RegisterType<UnitTestCryptoProvider>()
                .As<ICryptoProvider>()
                .SingleInstance();

            ContainerBuilder.RegisterType<UnitTestSerilogConfigProvider>()
                .As<ISerilogConfigProvider>()
                .SingleInstance();
        }
    }
}