using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MooMed.DotNet.Utils.Async
{
	public class ParallelAsyncRunner<T>
	{
		private readonly ConcurrentQueue<T> _workItemQueue;

		private readonly Func<T, Task> _asyncAction;

		public ParallelAsyncRunner(
			ConcurrentQueue<T> workItemQueue,
			Func<T, Task> asyncAction)
		{
			_workItemQueue = workItemQueue;
			_asyncAction = asyncAction;
		}

		public async Task<List<Exception>?> Run()
		{
			List<Exception>? exceptions = null;

			while (!_workItemQueue.IsEmpty)
			{
				var receivedWorkItem = _workItemQueue.TryDequeue(out var workItem);

				if (!receivedWorkItem)
				{
					continue;
				}

				if (workItem == null)
				{
					continue;
				}

				try
				{
					await _asyncAction(workItem);
				}
				catch (Exception e)
				{
					exceptions ??= new List<Exception>();

					exceptions.Add(e);
				}
			}

			return exceptions;
		}
	}
}