using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MooMed.DotNet.Extensions
{
	public static class AsyncLinqExtensions
	{
		public static async Task ParallelAsync<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, Task> asyncAction, int maxParallelTasks = 50)
		{
			var chunkedSourceData = enumerable.Split(maxParallelTasks);

			foreach (var dataChunk in chunkedSourceData)
			{
				await Task.WhenAll(dataChunk.Select(asyncAction));
			}
		}
		
		/// <summary>
		/// ValueTask ParallelAsync for compatibility reasons. Use if you know what you're doing, ValueTask isn't meant to be
		/// used like this...
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="asyncAction"></param>
		/// <param name="maxParallelTasks"></param>
		/// <returns></returns>
		public static async ValueTask ParallelAsyncValueTask<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, ValueTask> asyncAction, int maxParallelTasks = 50)
		{
			await enumerable.ParallelAsync(x => asyncAction(x).AsTask());
		}
	}
}
