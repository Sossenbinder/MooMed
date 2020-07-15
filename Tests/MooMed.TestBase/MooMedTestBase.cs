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
using MooMed.Logging.Module;
using MooMed.TestBase.Config;
using NUnit.Framework;

namespace MooMed.TestBase
{
    public class MooMedTestBase
    {
        protected string TestDir { get; private set; }

        protected IConfigSettingsProvider UnitTestSettingsProvider { get; private set; }

        // Can be replaced by deriving classes
        protected IContainer UnitTestContainer { get; set; }

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
            CreateAutoFacModule();

            TestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            UnitTestSettingsProvider = UnitTestContainer.Resolve<IConfigSettingsProvider>();
        }

        [SetUp]
        protected virtual void Setup()
        {

        }

        [TearDown]
        protected virtual void TearDown()
        {

        }

        protected virtual void TearDownFixture()
        {

        }

        private void CreateAutoFacModule()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<EncryptionModule>();
            builder.RegisterModule<ConfigurationModule>();
            builder.RegisterModule<LoggingModule>();

            var config = new ConfigurationBuilder()
	            .Add(new JsonConfigurationSource()
	            {
                    Path = "UnitTestSettings.json"
                })
	            .Build();

            builder
	            .Register(x => new Configuration.Config(config))
                .As<IConfig>()
                .SingleInstance();

            builder.RegisterType<UnitTestCryptoProvider>()
	            .As<ICryptoProvider>()
	            .SingleInstance();

	        UnitTestContainer = builder.Build();
        }
    }
}
