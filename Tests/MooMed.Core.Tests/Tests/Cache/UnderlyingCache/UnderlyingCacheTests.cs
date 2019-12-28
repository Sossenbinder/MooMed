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
		private IUnderlyingCache<Guid, string> m_underlyingCache;

		protected override void Setup()
		{
			base.Setup();

			m_underlyingCache = new UnderlyingMemoryCache<Guid, string>(5);
		}

		[Test]
		public void PutShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			Assert.False(m_underlyingCache.HasValue(key));

			m_underlyingCache.PutItem(key, "1234");
			
			Assert.True(m_underlyingCache.HasValue(key));
		}

		[Test]
		public void RemoveShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			m_underlyingCache.PutItem(key, "1234");

			Assert.True(m_underlyingCache.HasValue(key));

			m_underlyingCache.Remove(key);

			Assert.False(m_underlyingCache.HasValue(key));
		}

		[Test]
		public void GetItemShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			m_underlyingCache.PutItem(key, item);

			var itemFromCache = m_underlyingCache.GetItem(key);

			Assert.AreEqual(item, itemFromCache);
		}

		[Test]
		public void GetItemShouldReturnNullIfElementNotAvailable()
		{
			var key = Guid.NewGuid();

			var itemFromCache = m_underlyingCache.GetItem(key);

			Assert.AreEqual(null, itemFromCache);
		}

		[Test]
		public async Task GetItemLockedShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			m_underlyingCache.PutItem(key, item);

			var lockedItemFromCache = await m_underlyingCache.GetItemLocked(key);

			Assert.AreEqual(item, lockedItemFromCache.Payload);
		}

		[Test]
		public async Task GetItemLockedShouldTimeoutWhenNotReleasedAndQueriedAgain()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			m_underlyingCache.PutItem(key, item);

			await m_underlyingCache.GetItemLocked(key);

			Assert.False(await m_underlyingCache.GetItemLocked(key).WaitAsync(TimeSpan.FromSeconds(2)));
		}

		[Test]
		public async Task GetItemLockedShouldNotTimeoutWhenReleasedAndQueriedAgain()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			m_underlyingCache.PutItem(key, item);

			var lockedItem = await m_underlyingCache.GetItemLocked(key);
			lockedItem.Release();

			Assert.True(await m_underlyingCache.GetItemLocked(key).WaitAsync(TimeSpan.FromSeconds(2)));
		}
	}
}
