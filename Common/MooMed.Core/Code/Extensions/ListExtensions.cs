using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Extensions
{
	public static class ListExtensions
	{
		public static bool IsNullOrEmpty<T>([CanBeNull] this List<T> list)
		{
			return list == null || !list.Any();
		}
	}
}
