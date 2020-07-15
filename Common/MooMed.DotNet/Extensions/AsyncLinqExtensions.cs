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
			var chunkedSourceData = enumerable.Split(50);

			foreach (var dataChunk in chunkedSourceData)
			{
				await Task.WhenAll(dataChunk.Select(asyncAction));
			}
		}
	}
}
