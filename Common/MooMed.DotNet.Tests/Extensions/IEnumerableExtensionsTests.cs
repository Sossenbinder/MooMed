using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class IEnumerableExtensionsTests
	{
		[Test]
		public void IsNullOrEmptyShouldWork()
		{
			IEnumerable enumerable = "";
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = null!;
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = Enumerable.Empty<object>();
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = Enumerable.Range(0, 5);
			Assert.False(enumerable.IsNullOrEmpty());
		}

		[Test]
		public void IsNullOrEmptyShouldWorkForGeneric()
		{
			IEnumerable<object> enumerable = "".Cast<object>();
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = null!;
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = Enumerable.Empty<object>();
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = Enumerable.Range(0, 5).Cast<object>();
			Assert.False(enumerable.IsNullOrEmpty());
		}

		[Test]
		public void IsEmptyShouldWork()
		{
			IEnumerable enumerable = "";
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = null!;
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = Enumerable.Empty<object>();
			Assert.True(enumerable.IsNullOrEmpty());
		}

		[Test]
		public void IsEmptyShouldWorkForGeneric()
		{
			IEnumerable<object> enumerable = "".Cast<object>();
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = null!;
			Assert.True(enumerable.IsNullOrEmpty());

			enumerable = Enumerable.Empty<object>();
			Assert.True(enumerable.IsNullOrEmpty());
		}

		[Test]
		public void SplitShouldWorkForEvenlyFilledCollection()
		{
			var collectionToSplit = Enumerable
				.Range(0, 10);

			var splitArray = collectionToSplit.Split(5).ToList();

			Assert.IsTrue(splitArray.Count == 2);
			Assert.IsTrue(splitArray.TrueForAll(x => x.Count() == 5));
		}

		[Test]
		public void SplitShouldWorkForUnEvenlyFilledCollection()
		{
			var collectionToSplit = Enumerable
				.Range(0, 9);

			var splitArray = collectionToSplit.Split(5).ToList();

			Assert.IsTrue(splitArray.Count == 2);
			Assert.IsTrue(splitArray[0].Count() == 5);
			Assert.IsTrue(splitArray[1].Count() == 4);
		}

		[Test]
		public void SplitShouldWorkForCollectionLessThanLimit()
		{
			var collectionToSplit = Enumerable
				.Range(0, 3);

			var splitArray = collectionToSplit.Split(5).ToList();

			Assert.IsTrue(splitArray.Count == 1);
			Assert.IsTrue(splitArray[0].Count() == 3);
		}
	}
}