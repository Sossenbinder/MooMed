using MooMed.Caching.Cache.UnderlyingCache.Locking;
using MooMed.DotNet.Utils.Async;
using MooMed.Serialization.Service.Interface;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace MooMed.Caching.Cache.UnderlyingCache
{
    public class UnderlyingRedisCache<TKey, TValue> : AbstractUnderlyingCache<TKey, TValue>
        where TKey : notnull

    {
        private readonly AsyncLazy<IConnectionMultiplexer> _connectionMultiPlexer;

        private readonly ISerializationService _serializationService;

        public UnderlyingRedisCache(
            AsyncLazy<IConnectionMultiplexer> connectionMultiPlexer,
            ISerializationService serializationService)
            : base(new RedisCacheLockManager<TKey>(connectionMultiPlexer))
        {
            _connectionMultiPlexer = connectionMultiPlexer;
            _serializationService = serializationService;
        }

        public override async ValueTask PutItem(TKey key, TValue value, int? secondsToLive = null)
        {
            var stringKey = key.ToString();

            var db = await GetDb();

            var valueSerialized = _serializationService.Serialize(value);

            var result = await db.StringSetAsync(stringKey, valueSerialized);
        }

        public override async ValueTask<TValue> GetItem(TKey key)
        {
            var stringKey = key.ToString();

            var db = await GetDb();

            var redisValue = await db.StringGetAsync(stringKey);

            if (!redisValue.HasValue)
            {
                return default;
            }

            var deserializedValue = _serializationService.Deserialize<TValue>(redisValue);

            return deserializedValue;
        }

        public override async ValueTask Remove(TKey key)
        {
            var stringKey = key.ToString();

            var db = await GetDb();

            await db.KeyDeleteAsync(stringKey);
        }

        public override async ValueTask<bool> HasValue(TKey key)
        {
            var stringKey = key.ToString();

            var db = await GetDb();

            return await db.KeyExistsAsync(stringKey);
        }

        private async ValueTask<IDatabase> GetDb() => (await _connectionMultiPlexer.Value).GetDatabase();
    }
}