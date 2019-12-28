using System.IO;
using System.Reflection;
using Autofac;
using MooMed.Core;
using MooMed.Core.Code.Configuration.Interface;
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

            builder.RegisterModule(new CoreBindings());

            builder
                .RegisterType<UnitTestSettingsAccessor>()
                .As<IConfigSettingsAccessor>()
                .SingleInstance();

            UnitTestContainer = builder.Build();
        }
    }
}
