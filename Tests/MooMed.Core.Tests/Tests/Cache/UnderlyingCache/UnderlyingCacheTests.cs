using System;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.Core.Code.Extensions;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Core.Tests.Tests.Cache.UnderlyingCache
{
	[TestFixture]
	public class UnderlyingCacheTests : MooMedTestBase
	{
		private IUnderlyingCache<Guid, string> _underlyingCache;

		protected override void Setup()
		{
			base.Setup();

			_underlyingCache = new UnderlyingMemoryCache<Guid, string>(5);
		}

		[Test]
		public void PutShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			Assert.False(_underlyingCache.HasValue(key));

			_underlyingCache.PutItem(key, "1234");
			
			Assert.True(_underlyingCache.HasValue(key));
		}

		[Test]
		public void RemoveShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			_underlyingCache.PutItem(key, "1234");

			Assert.True(_underlyingCache.HasValue(key));

			_underlyingCache.Remove(key);

			Assert.False(_underlyingCache.HasValue(key));
		}

		[Test]
		public void GetItemShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			_underlyingCache.PutItem(key, item);

			var itemFromCache = _underlyingCache.GetItem(key);

			Assert.AreEqual(item, itemFromCache);
		}

		[Test]
		public void GetItemShouldReturnNullIfElementNotAvailable()
		{
			var key = Guid.NewGuid();

			var itemFromCache = _underlyingCache.GetItem(key);

			Assert.AreEqual(null, itemFromCache);
		}

		[Test]
		public async Task GetItemLockedShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			_underlyingCache.PutItem(key, item);

			var lockedItemFromCache = await _underlyingCache.GetItemLocked(key);

			Assert.AreEqual(item, lockedItemFromCache.Payload);
		}

		[Test]
		public async Task GetItemLockedShouldTimeoutWhenNotReleasedAndQueriedAgain()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			_underlyingCache.PutItem(key, item);

			await _underlyingCache.GetItemLocked(key);

			Assert.False(await _underlyingCache.GetItemLocked(key).WaitAsync(TimeSpan.FromSeconds(2)));
		}

		[Test]
		public async Task GetItemLockedShouldNotTimeoutWhenReleasedAndQueriedAgain()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			_underlyingCache.PutItem(key, item);

			var lockedItem = await _underlyingCache.GetItemLocked(key);
			lockedItem.Release();

			Assert.True(await _underlyingCache.GetItemLocked(key).WaitAsync(TimeSpan.FromSeconds(2)));
		}
	}
}
