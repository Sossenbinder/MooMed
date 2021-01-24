using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions;
using MooMed.Eventing.Events;
using NUnit.Framework;

namespace MooMed.Core.Tests.Tests.Events.Local
{
	[TestFixture]
	public class ServiceLocalEventTests : TestBase.TestBase
	{
		private ServiceLocalEvent _serviceLocalEvent = null!;

		protected override void Setup()
		{
			base.Setup();

			_serviceLocalEvent = new ServiceLocalEvent();
		}

		[Test]
		public void RegisteringShouldWork()
		{
			var testFunc = new Func<Task>(() => Task.CompletedTask);

			_serviceLocalEvent.Register(testFunc);

			Assert.NotZero(_serviceLocalEvent.GetAllRegisteredEvents().Count);
		}

		[Test]
		public void UnRegisteringShouldWork()
		{
			var testFunc = new Func<Task>(() => Task.CompletedTask);

			_serviceLocalEvent.Register(testFunc);

			Assert.NotZero(_serviceLocalEvent.GetAllRegisteredEvents().Count);

			_serviceLocalEvent.Unregister(testFunc);

			Assert.Zero(_serviceLocalEvent.GetAllRegisteredEvents().Count);
		}

		[Test]
		public async Task RaisingShouldWork()
		{
			var wasCalled = false;

			var testFunc = new Func<Task>(() =>
			{
				wasCalled = true;

				return Task.CompletedTask;
			});

			_serviceLocalEvent.Register(testFunc);

			await _serviceLocalEvent.Raise();

			Assert.IsTrue(wasCalled);
		}

		[Test]
		public void FireAndForgetRaisingShouldWork()
		{
			var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
			var cancellationTokenSource = new CancellationTokenSource(1000);

			var testFunc = new Func<Task>(() =>
			{
				tcs.SetResult();
				return Task.CompletedTask;
			});

			_serviceLocalEvent.Register(testFunc);

			_serviceLocalEvent.RaiseFireAndForget(Logger);

			async Task CancellationTokenTask() => await cancellationTokenSource.Token;

			// CancellationToken would throw in case the event is not called in time
			Assert.DoesNotThrowAsync(() => Task.WhenAny(CancellationTokenTask(), tcs.Task));
		}

		[Test]
		public async Task MultipleExceptionsShouldResultInAggregate()
		{
			var range = Enumerable.Range(0, 10).ToList();

			var handlers = range
				.Select(x =>
				{
					return new Func<Task>(() => throw new Exception(x.ToString()));
				})
				.ToList();

			foreach (var handler in handlers)
			{
				_serviceLocalEvent.Register(handler);
			}

			try
			{
				await _serviceLocalEvent.Raise();
				Assert.Fail();
			}
			catch (AggregateException exception)
			{
				var subExceptions = exception.InnerExceptions;

				Assert.IsNotEmpty(subExceptions);
				Assert.AreEqual(subExceptions.Count, handlers.Count);

				foreach (var (exc, index) in subExceptions.Select((x, i) => (x, i)))
				{
					Assert.AreEqual(int.Parse(exc.Message), index);
				}
			}
		}
	}
}