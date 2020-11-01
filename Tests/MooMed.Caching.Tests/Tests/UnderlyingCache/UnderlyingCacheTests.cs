using System;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.DotNet.Extensions;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Caching.Tests.Tests.UnderlyingCache
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
		public async Task PutShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			Assert.False(await _underlyingCache.HasValue(key));

			await _underlyingCache.PutItem(key, "1234");

			Assert.True(await _underlyingCache.HasValue(key));
		}

		[Test]
		public async Task RemoveShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			await _underlyingCache.PutItem(key, "1234");

			Assert.True(await _underlyingCache.HasValue(key));

			await _underlyingCache.Remove(key);

			Assert.False(await _underlyingCache.HasValue(key));
		}

		[Test]
		public async Task GetItemShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _underlyingCache.PutItem(key, item);

			var itemFromCache = await _underlyingCache.GetItem(key);

			Assert.AreEqual(item, itemFromCache);
		}

		[Test]
		public async Task GetItemShouldReturnNullIfElementNotAvailable()
		{
			var key = Guid.NewGuid();

			var itemFromCache = await _underlyingCache.GetItem(key);

			Assert.AreEqual(null, itemFromCache);
		}

		[Test]
		public async Task GetItemLockedShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _underlyingCache.PutItem(key, item);

			var lockedItemFromCache = await _underlyingCache.GetItemLocked(key);

			Assert.AreEqual(item, lockedItemFromCache.Payload);
		}

		[Test]
		public async Task GetItemLockedShouldUnlockOnDispose()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _underlyingCache.PutItem(key, item);

			await using (await _underlyingCache.GetItemLocked(key))
			{
				// Doing nothing
			}

			var proofTimer = Task.Delay(TimeSpan.FromSeconds(1));
			var lockAquirationTask = _underlyingCache.GetItemLocked(key);

			await Task.WhenAny(proofTimer, lockAquirationTask.AsTask());

			// If the cache item is unlocked, the lock aquiration shouldn't run into a timeout and therefore be available immediately here.
			// If the prooftimer finishes first, then the other Task is still stuck in aquiring the lock which hints at it having to wait for lock resolution
			Assert.That(!proofTimer.IsCompleted && lockAquirationTask.IsCompleted);
		}

		[Test]
		public async Task GetItemLockedShouldUnlockOnAutoUnlock()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _underlyingCache.PutItem(key, item);

			var lockHold = await _underlyingCache.GetItemLocked(key);

			// Lock should timeout after
			await Task.Delay(TimeSpan.FromSeconds(6));

			var proofTimer = Task.Delay(TimeSpan.FromSeconds(1));
			var lockAquirationTask = _underlyingCache.GetItemLocked(key);

			await Task.WhenAny(proofTimer, lockAquirationTask.AsTask());

			// If the cache item is unlocked, the lock aquiration shouldn't run into a timeout and therefore be available immediately here.
			// If the prooftimer finishes first, then the other Task is still stuck in aquiring the lock which hints at it having to wait for lock resolution
			Assert.That(!proofTimer.IsCompleted && lockAquirationTask.IsCompleted);
		}

		[Test]
		public async Task GetItemLockedShouldNotTimeoutWhenReleasedAndQueriedAgain()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _underlyingCache.PutItem(key, item);

			var lockedItem = await _underlyingCache.GetItemLocked(key);
			lockedItem.Release();

			Assert.True(await _underlyingCache.GetItemLocked(key).WaitAsync(TimeSpan.FromSeconds(2)));
		}
	}
}