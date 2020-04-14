using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooMed.Core.Code.Extensions
{
	public static class AsyncLinqExtensions
	{
		public static Task ForEachAsync<T>([NotNull] this IEnumerable<T> enumerable, Func<T, Task> action)
		{
			var tasks = enumerable.Select(action);

			return Task.WhenAll(tasks);
		}
	}
}
