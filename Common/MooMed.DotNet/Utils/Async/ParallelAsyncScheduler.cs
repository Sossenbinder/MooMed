using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MooMed.DotNet.Utils.Async
{
	public class ParallelAsyncScheduler<T>
	{
		private readonly ConcurrentQueue<T> _concurrentQueue;

		private readonly Func<T, Task> _asyncAction;

		private readonly int _parallelTaskCount;

		private ParallelAsyncScheduler(
			IEnumerable<T> dataSet,
			Func<T, Task> asyncAction,
			int parallelTaskCount = 30)
		{
			_concurrentQueue = new ConcurrentQueue<T>(dataSet);
			_asyncAction = asyncAction;
			_parallelTaskCount = parallelTaskCount > _concurrentQueue.Count ? _concurrentQueue.Count : parallelTaskCount;
		}

		public static Task Run(
			IEnumerable<T> dataSet,
			Func<T, Task> asyncAction,
			int parallelTaskCount = 30)
		{
			var parallelAsyncRunner = new ParallelAsyncScheduler<T>(dataSet, asyncAction, parallelTaskCount);

			return parallelAsyncRunner.RunInternal();
		}

		private async Task RunInternal()
		{
			var taskRunners = new List<ParallelAsyncRunner<T>>();
			for (var i = 0; i < _parallelTaskCount; ++i)
			{
				taskRunners.Add(new ParallelAsyncRunner<T>(_concurrentQueue, _asyncAction));
			}

			var runnerExceptionTasks = taskRunners
				.Select(x => x.Run())
				.ToList();

			await Task.WhenAll(runnerExceptionTasks);

			var exceptions = runnerExceptionTasks
				.Where(x => x.Result != null)
				.SelectMany(x => x.Result)
				.ToList();

			if (exceptions.Any())
			{
				throw new AggregateException();
			}
		}
	}
}