using System;
using System.Threading.Tasks;
using MooMed.Eventing.Events;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Core.Tests.Tests.Events
{
	[TestFixture]
	public class MooEventTests : MooMedTestBase
	{
		private ServiceLocalMooEvent<int> _serviceLocalMooEvent;

		protected override void Setup()
		{
			base.Setup();

			_serviceLocalMooEvent = new ServiceLocalMooEvent<int>();
		}

		[Test]
		public void RegisteringShouldWork()
		{
			var testFunc = new Func<int, Task>(args => Task.CompletedTask);

			_serviceLocalMooEvent.Register(testFunc);

			Assert.NotZero(_serviceLocalMooEvent.GetAllRegisteredEvents().Count);
		}

		[Test]
		public void UnRegisteringShouldWork()
		{
			var testFunc = new Func<int, Task>((args) => Task.CompletedTask);

			_serviceLocalMooEvent.Register(testFunc);

			Assert.NotZero(_serviceLocalMooEvent.GetAllRegisteredEvents().Count);

			_serviceLocalMooEvent.UnRegister(testFunc);

			Assert.Zero(_serviceLocalMooEvent.GetAllRegisteredEvents().Count);
		}
	}
}