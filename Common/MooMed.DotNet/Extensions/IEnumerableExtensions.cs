﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MooMed.DotNet.Extensions
{
	// ReSharper disable once InconsistentNaming
	public static class IEnumerableExtensions
	{
		public static bool IsNullOrEmpty(this IEnumerable? enumerable) => enumerable?.Cast<object>().IsNullOrEmpty() ?? true;

		public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) => enumerable == null || !enumerable.Any();

		public static bool IsEmpty(this IEnumerable enumerable) => enumerable.Cast<object>().IsEmpty();

		public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> enumerable, int maxSize)
		{
			var chunk = new List<T>();

			foreach (var t in enumerable)
			{
				chunk.Add(t);

				if (chunk.Count % maxSize != 0)
				{
					continue;
				}

				yield return chunk;
				chunk = new List<T>();
			}

			if (chunk.Count > 0)
			{
				yield return chunk;
			}
		}
	}
}