using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class AsyncLinqExtensionsTests
	{
		[Test]
		public async Task ParallelAsyncShouldWorkForHappyPath()
		{
			var parallelWorkItems = Enumerable
				.Range(0, 15)
				.ToList();

			var processedItems = new ConcurrentBag<int>();

			await parallelWorkItems.ParallelAsync(async x =>
			{
				await Task.Delay(200);

				processedItems.Add(x);
			}, 5);

			Assert.True(processedItems.Count == parallelWorkItems.Count);
		}

		[Test]
		public async Task ParallelAsyncShouldProperlyChunkWork()
		{
			var delayInMs = 200;
			var parallelTaskCount = 10;

			var parallelWorkItems = Enumerable
				.Range(0, 50)
				.ToList();

			var processedItems = new ConcurrentBag<int>();

			var parallelTask = parallelWorkItems.ParallelAsync(async x =>
			{
				await Task.Delay(delayInMs);

				processedItems.Add(x);
			}, parallelTaskCount);

			// Let's wait for the first batch which started instantly to do it's work
			await Task.Delay((int)(1.5 * delayInMs));
			Assert.True(processedItems.Count == parallelTaskCount);

			// Let's wait for the second batch which continues and check again. As we are already "in between"
			// two sections of work time-wise, we don't need to wait 1.5s again
			await Task.Delay(delayInMs);
			Assert.True(processedItems.Count == 2 * parallelTaskCount);

			await parallelTask;

			Assert.True(processedItems.Count == parallelWorkItems.Count);
		}

		[Test]
		public async Task ParallelAsyncShouldWorkWithMoreWorkersThanRequired()
		{
			var parallelWorkItems = Enumerable
				.Range(0, 10)
				.ToList();

			var processedItems = new ConcurrentBag<int>();

			await parallelWorkItems.ParallelAsync(async x =>
			{
				await Task.Delay(200);

				processedItems.Add(x);
			}, 20);

			Assert.True(processedItems.Count == parallelWorkItems.Count);
		}

		[Test]
		public void ParallelAsyncShouldThrowIfExceptionThrown()
		{
			var parallelWorkItems = Enumerable
				.Range(0, 5)
				.ToList();

			Assert.CatchAsync<AggregateException>(() => parallelWorkItems.ParallelAsync(async x =>
			{
				await Task.Delay(200);

				throw new Exception();
			}, 20));
		}

		[Test]
		public async Task ParallelAsyncShouldContainExceptionIfExceptionThrown()
		{
			var parallelWorkItems = Enumerable
				.Range(0, 5)
				.ToList();

			try
			{
				await parallelWorkItems.ParallelAsync(async x =>
				{
					await Task.Delay(200);

					throw new ArgumentNullException();
				}, 20);
			}
			catch (AggregateException exc)
			{
				var unwrappedExceptions = exc.InnerExceptions;

				foreach (var innerExc in unwrappedExceptions)
				{
					Assert.True(innerExc is ArgumentNullException);
				}
			}
		}
	}
}