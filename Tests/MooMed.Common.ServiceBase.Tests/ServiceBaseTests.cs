using System;
using System.Threading.Tasks;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.DotNet.Utils.Disposable;
using MooMed.Eventing.Events.Interface;
using MooMed.TestBase;
using MooMed.TestBase.Utils;
using Moq;
using NUnit.Framework;

namespace MooMed.Common.ServiceBase.Tests
{
    public class ServiceBaseTests : MooMedTestBase
    {
        private Mock<ILocalEvent<string>> _serviceLocalEvent;

        private Mock<IDistributedEvent<string>> _distributedEvent;

        internal class TestServiceBase : MooMedServiceBase
        {
            public TestServiceBase(IEvent<string> @event)
            {
                RegisterEventHandler(@event, TestHandler);
            }
        }

        protected override void Setup()
        {
            _serviceLocalEvent = new Mock<ILocalEvent<string>>();

            _distributedEvent = new Mock<IDistributedEvent<string>>();
        }

        [Test]
        public void ServiceBaseShouldDisposeLocalEventHandlersOnDisposal()
        {
            //var serviceLocalRegistrationVerifier = VerifiableMockSetup.Setup(_serviceLocalEvent,
            //    x => x.Register(It.IsAny<Func<string, Task>>()),
            //    x => new DisposableAction(() => x.UnRegister(TestHandler)));

            _serviceLocalEvent.Setup(x => x.Register(It.IsAny<Func<string, Task>>()))
                .Callback(() =>
                {
                })
                .Returns((ILocalEvent<string> x) => new DisposableAction(() => x.UnRegister(TestHandler)));
            var serviceLocalUnRegistrationVerifier = VerifiableMockSetup.Setup(_serviceLocalEvent, x => x.UnRegister(TestHandler));

            using (new TestServiceBase(_serviceLocalEvent.Object))
            {
                //serviceLocalRegistrationVerifier.Verify();
            }

            serviceLocalUnRegistrationVerifier.Verify();
        }

        private static Task TestHandler(string _) => Task.CompletedTask;
    }
}