using System.Threading.Tasks;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Caching.Tests.Tests
{
	[TestFixture]
	public class CacheTests : MooMedTestBase
	{
		private ICache<string> _objectCache;

		private string _cacheKey = "key1234";

		protected override void Setup()
		{
			base.TearDown();

			_objectCache = new Cache<string>(new UnderlyingMemoryCache<string, string>(new CacheSettings(60, "UnitTest").TtlInSeconds));
		}

		[Test]
		public async Task ItemShouldBeInCacheAfterPut()
		{
			const string value = "blabla";

			await _objectCache.PutItem(_cacheKey, value);

			Assert.IsTrue(await _objectCache.HasValue(_cacheKey));
		}

		[Test]
		public async Task ItemShouldBeGoneFromCacheAfterRemove()
		{
			const string value = "blabla";

			await _objectCache.PutItem(_cacheKey, value);
			await _objectCache.Remove(_cacheKey);

			Assert.IsFalse(await _objectCache.HasValue(_cacheKey));
		}

		[Test]
		public async Task RemovingItemNotInCacheShouldFailSilently()
		{
			await _objectCache.Remove(_cacheKey);
		}

		[Test]
		public async Task ItemShouldBeRetrievableFromCacheAfterPut()
		{
			const string value = "blabla";

			await _objectCache.PutItem(_cacheKey, value);

			var item = await _objectCache.GetItem(_cacheKey);

			Assert.NotNull(item);
			Assert.IsTrue(value.Equals(item));
		}

		[Test]
		public async Task ItemShouldBeReplacedWhenPutTwice()
		{
			const string value = "blabla";

			await _objectCache.PutItem(_cacheKey, value);

			var replacementValue = "blablabla2";

			await _objectCache.PutItem(_cacheKey, replacementValue);

			var item = await _objectCache.GetItem(_cacheKey);

			Assert.IsTrue(item.Equals(replacementValue));
		}

		[Test]
		public async Task ItemShouldBeRemovedAfterTtl()
		{
			const string value = "blabla";

			await _objectCache.PutItem(_cacheKey, value, 1);

			await Task.Delay(5000);

			Assert.IsFalse(await _objectCache.HasValue(_cacheKey));
		}
	}
}