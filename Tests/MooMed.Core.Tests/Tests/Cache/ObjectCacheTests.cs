using System.Threading.Tasks;
using MooMed.Caching.Cache.CacheImplementations;
using MooMed.Caching.Cache.CacheImplementations.Interface;
using MooMed.Caching.Cache.CacheInformation;
using MooMed.TestBase;
using NUnit.Framework;

namespace MooMed.Core.Tests.Tests.Cache
{
    [TestFixture]
    public class ObjectCacheTests : MooMedTestBase
    {
        private ICache<string> _objectCache;

        private string _cacheKey = "key1234";

        protected override void SetupFixture()
        {
            base.SetupFixture();
        }

        protected override void Setup()
        {
            base.TearDown();

            _objectCache = new ObjectCache<string>(new CacheSettings(60, "UnitTest"));
        }

        [Test]
        public void ItemShouldBeInCacheAfterPut()
        {
            const string value = "blabla";

            _objectCache.PutItem(_cacheKey, value);

            Assert.IsTrue(_objectCache.HasValue(_cacheKey));
        }

        [Test]
        public void ItemShouldBeGoneFromCacheAfterRemove()
        {
            const string value = "blabla";

            _objectCache.PutItem(_cacheKey, value);
            _objectCache.Remove(_cacheKey);

            Assert.IsFalse(_objectCache.HasValue(_cacheKey));
        }

        [Test]
        public void RemovingItemNotInCacheShouldFailSilently()
        {
            _objectCache.Remove(_cacheKey);
        }

        [Test]
        public void ItemShouldBeRetrievableFromCacheAfterPut()
        {
            const string value = "blabla";

            _objectCache.PutItem(_cacheKey, value);

            var item = _objectCache.GetItem(_cacheKey);

            Assert.NotNull(item);
            Assert.IsTrue(value.Equals(item));
        }

        [Test]
        public void ItemShouldBeReplacedWhenPutTwice()
        {
            const string value = "blabla";

            _objectCache.PutItem(_cacheKey, value);

            var replacementValue = "blablabla2";

            _objectCache.PutItem(_cacheKey, replacementValue);

            var item = _objectCache.GetItem(_cacheKey);

            Assert.IsTrue(item.Equals(replacementValue));
        }

        [Test]
        public async Task ItemShouldBeRemovedAfterTtl()
        {
            const string value = "blabla";

            _objectCache.PutItem(_cacheKey, value, 1);

            await Task.Delay(5000);

            Assert.IsFalse(_objectCache.HasValue(_cacheKey));
        }
    }
}
