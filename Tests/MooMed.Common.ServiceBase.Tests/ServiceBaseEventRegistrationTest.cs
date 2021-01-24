using System;
using System.Threading.Tasks;
using MooMed.Common.ServiceBase.ServiceBase;
using MooMed.DotNet.Utils.Disposable;
using MooMed.Eventing.Events.Interface;
using MooMed.TestBase.Utils;
using Moq;
using NUnit.Framework;

namespace MooMed.Common.ServiceBase.Tests
{
	public class ServiceBaseEventRegistrationTest : TestBase.TestBase
	{
		private Mock<IEvent<string>> _serviceLocalEvent;

		internal class TestServiceBase : ServiceBase.ServiceBase
		{
			public TestServiceBase(IEvent<string> @event)
			{
				RegisterEventHandler(@event, TestHandler);
			}
		}

		protected override void Setup()
		{
			base.Setup();

			_serviceLocalEvent = new Mock<IEvent<string>>();
		}

		[Test]
		public void ServiceBaseShouldDisposeLocalEventHandlersOnDisposal()
		{
			var serviceLocalRegistrationVerifier = VerifiableMockSetup.Setup(_serviceLocalEvent,
				x => x.Register(It.IsAny<Func<string, Task>>()),
				() => new DisposableAction(() => _serviceLocalEvent.Object.Unregister(TestHandler)));

			var serviceLocalUnRegistrationVerifier = VerifiableMockSetup.Setup(_serviceLocalEvent, x => x.Unregister(TestHandler));

			using (new TestServiceBase(_serviceLocalEvent.Object))
			{
				serviceLocalRegistrationVerifier.Verify();
			}

			serviceLocalUnRegistrationVerifier.Verify();
		}

		private static Task TestHandler(string _) => Task.CompletedTask;
	}
}