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
        private ICache<string> m_objectCache;

        private string m_cacheKey = "key1234";

        protected override void SetupFixture()
        {
            base.SetupFixture();
        }

        protected override void Setup()
        {
            base.TearDown();

            m_objectCache = new ObjectCache<string>(new CacheSettings(60, "UnitTest"));
        }

        [Test]
        public void ItemShouldBeInCacheAfterPut()
        {
            const string value = "blabla";

            m_objectCache.PutItem(m_cacheKey, value);

            Assert.IsTrue(m_objectCache.HasValue(m_cacheKey));
        }

        [Test]
        public void ItemShouldBeGoneFromCacheAfterRemove()
        {
            const string value = "blabla";

            m_objectCache.PutItem(m_cacheKey, value);
            m_objectCache.Remove(m_cacheKey);

            Assert.IsFalse(m_objectCache.HasValue(m_cacheKey));
        }

        [Test]
        public void RemovingItemNotInCacheShouldFailSilently()
        {
            m_objectCache.Remove(m_cacheKey);
        }

        [Test]
        public void ItemShouldBeRetrievableFromCacheAfterPut()
        {
            const string value = "blabla";

            m_objectCache.PutItem(m_cacheKey, value);

            var item = m_objectCache.GetItem(m_cacheKey);

            Assert.NotNull(item);
            Assert.IsTrue(value.Equals(item));
        }

        [Test]
        public void ItemShouldBeReplacedWhenPutTwice()
        {
            const string value = "blabla";

            m_objectCache.PutItem(m_cacheKey, value);

            var replacementValue = "blablabla2";

            m_objectCache.PutItem(m_cacheKey, replacementValue);

            var item = m_objectCache.GetItem(m_cacheKey);

            Assert.IsTrue(item.Equals(replacementValue));
        }

        [Test]
        public async Task ItemShouldBeRemovedAfterTtl()
        {
            const string value = "blabla";

            m_objectCache.PutItem(m_cacheKey, value, 1);

            await Task.Delay(5000);

            Assert.IsFalse(m_objectCache.HasValue(m_cacheKey));
        }
    }
}
