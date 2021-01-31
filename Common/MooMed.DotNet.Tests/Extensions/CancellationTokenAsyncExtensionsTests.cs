using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class CancellationTokenAsyncExtensionsTests
	{
		[Test]
		public void CancellationTokenShouldBeAwaitable()
		{
			var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

			Assert.ThrowsAsync<OperationCanceledException>(async () => await cts.Token);
		}

		[Test]
		public async Task CancellationTokenAwaiterShouldNotThrowWithWhenAnyForShorterTask()
		{
			var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));

			static async Task ShorterTask() => await Task.Yield();

			await Task.WhenAny(GetAwaitableTask(cts.Token), ShorterTask());
		}

		[Test]
		public void CancellationTokenAwaiterShouldCancelWhenAnyForLongerTask()
		{
			var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(250));

			static Task LongerTask() => Task.Delay(TimeSpan.FromMilliseconds(10000));

			Assert.ThrowsAsync<OperationCanceledException>(async () =>
			{
				var longerTask = LongerTask();
				var awaitableTokenTask = GetAwaitableTask(cts.Token);
				var cancelledTask = await Task.WhenAny(awaitableTokenTask, longerTask);

				Assert.AreEqual(cancelledTask.Id, awaitableTokenTask.Id);

				await cancelledTask;
			});
		}

		public static async Task GetAwaitableTask(CancellationToken ct)
		{
			await ct;
		}
	}
}