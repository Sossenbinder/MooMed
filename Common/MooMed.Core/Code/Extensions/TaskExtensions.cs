using System;
using System.Threading.Tasks;

namespace MooMed.Core.Code.Extensions
{
	public static class TaskExtensions
	{
		public static async Task<bool> WaitAsync(this Task task, TimeSpan timeSpan)
		{
			await Task.Delay(timeSpan);

			return task.IsCompleted;
		}
	}
}
