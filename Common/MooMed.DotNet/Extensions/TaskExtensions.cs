using System;
using System.Threading.Tasks;

namespace MooMed.DotNet.Extensions
{
	public static class TaskExtensions
	{
		public static async Task<bool> WaitAsync(this Task task, TimeSpan timeSpan)
		{
			await Task.Delay(timeSpan);

			return task.IsCompleted;
		}

		public static Task IgnoreTaskCancelledException(this Task task)
		{
			return task.ContinueWith(continuedTask => continuedTask.Exception?.Handle(ex => ex is TaskCanceledException));
		}
	}
}