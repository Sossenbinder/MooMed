using System;
using System.Threading.Tasks;

namespace MooMed.DotNet.Extensions
{
	public static class ValueTaskExtensions
	{
		public static async ValueTask<bool> WaitAsync(this ValueTask task, TimeSpan timeSpan)
			=> await task.AsTask().WaitAsync(timeSpan);

		public static async ValueTask<bool> WaitAsync<T>(this ValueTask<T> task, TimeSpan timeSpan)
			=> await task.AsTask().WaitAsync(timeSpan);
	}
}