using System;
using System.Threading.Tasks;
using MooMed.Caching.Cache.UnderlyingCache;
using MooMed.Caching.Cache.UnderlyingCache.Interface;
using MooMed.DotNet.Extensions;
using NUnit.Framework;

namespace MooMed.Caching.Tests.Tests.UnderlyingCache
{
	[TestFixture]
	public class MemoryCacheTests : TestBase.TestBase
	{
		private ICacheImplementation<Guid, string> _cacheImplementation = null!;

		private Func<ICacheImplementation<Guid, string>> _cacheGenerator;

		public MemoryCacheTests()
		{
			_cacheGenerator = () => new MemoryCacheImplementation<Guid, string>(5);
		}

		protected override void Setup()
		{
			base.Setup();

			_cacheImplementation = _cacheGenerator();
		}

		[Test]
		public async Task PutShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			Assert.False(await _cacheImplementation.HasValue(key));

			await _cacheImplementation.PutItem(key, "1234");

			Assert.True(await _cacheImplementation.HasValue(key));
		}

		[Test]
		public async Task RemoveShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();

			await _cacheImplementation.PutItem(key, "1234");

			Assert.True(await _cacheImplementation.HasValue(key));

			await _cacheImplementation.Remove(key);

			Assert.False(await _cacheImplementation.HasValue(key));
		}

		[Test]
		public async Task GetItemShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _cacheImplementation.PutItem(key, item);

			var itemFromCache = await _cacheImplementation.GetItem(key);

			Assert.AreEqual(item, itemFromCache);
		}

		[Test]
		public async Task GetItemShouldReturnNullIfElementNotAvailable()
		{
			var key = Guid.NewGuid();

			var itemFromCache = await _cacheImplementation.GetItem(key);

			Assert.AreEqual(null, itemFromCache);
		}

		[Test]
		public async Task GetItemLockedShouldWorkForHappyPath()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _cacheImplementation.PutItem(key, item);

			var lockedItemFromCache = await _cacheImplementation.GetItemLocked(key);

			Assert.AreEqual(item, lockedItemFromCache.Payload);
		}

		[Test]
		public async Task GetItemLockedShouldUnlockOnDispose()
		{
			var key = Guid.NewGuid();
			var item = "1234";

			await _cacheImplementation.PutItem(key, item);

			await using (await _cacheImplementation.GetItemLocked(key))
			{
				// Doing nothing
			}

			var proofTimer = Task.Delay(TimeSpan.FromSeconds(1));
			var lockAquirationTask = _cacheImplementation.GetItemLocked(key);

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

			await _cacheImplementation.PutItem(key, item);

			var lockHold = await _cacheImplementation.GetItemLocked(key);

			// Lock should timeout after
			await Task.Delay(TimeSpan.FromSeconds(6));

			var proofTimer = Task.Delay(TimeSpan.FromSeconds(1));
			var lockAquirationTask = _cacheImplementation.GetItemLocked(key);

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

			await _cacheImplementation.PutItem(key, item);

			var lockedItem = await _cacheImplementation.GetItemLocked(key);
			await lockedItem.Release();

			Assert.True(await _cacheImplementation.GetItemLocked(key).WaitAsync(TimeSpan.FromSeconds(2)));
		}
	}
}