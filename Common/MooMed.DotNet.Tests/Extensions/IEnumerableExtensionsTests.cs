using System.Linq;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Extensions
{
	[TestFixture]
	public class IEnumerableExtensionsTests
	{
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
