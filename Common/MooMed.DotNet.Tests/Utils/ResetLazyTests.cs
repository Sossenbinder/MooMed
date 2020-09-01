using System;
using System.Threading.Tasks;
using MooMed.DotNet.Utils.ResetLazy;
using NUnit.Framework;

namespace MooMed.DotNet.Tests.Utils
{
	[TestFixture]
	public class ResetLazyTests
	{
		[Test]
		public void ResetLazyShouldWorkLikeRegularLazy()
		{
			var valueToCreate = 1;
			int TaskCreator() => valueToCreate;

			var regularLazy = new Lazy<int>(TaskCreator);
			var customResetLazy = new ResetLazy<int>(TaskCreator);

			Assert.AreEqual(regularLazy.IsValueCreated, customResetLazy.IsValueCreated);
			Assert.AreEqual(regularLazy.Value, customResetLazy.Value());
			Assert.AreEqual(regularLazy.IsValueCreated, customResetLazy.IsValueCreated);
		}

		[Test]
		public void ResetLazyShouldNotCacheOnException()
		{
			static int TaskCreator() => throw new Exception();

			var regularLazy = new Lazy<int>(TaskCreator);
			var customResetLazy = new ResetLazy<int>(TaskCreator);

			Assert.AreEqual(regularLazy.IsValueCreated, customResetLazy.IsValueCreated);

			int val1;
			Assert.Throws<Exception>(() => val1 = regularLazy.Value);

			int val2;
			Assert.Throws<Exception>(() => val2 = customResetLazy.Value());

			Assert.AreEqual(regularLazy.IsValueCreated, customResetLazy.IsValueCreated);
		}
	}
}