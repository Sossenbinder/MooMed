using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using MooMed.Caching.ReliableCollection.Interface;

namespace MooMed.Caching.ReliableCollection
{
    public class ReliableObjectCache<TValue> : IReliableCache<TValue>
    {
        [NotNull]
        private readonly IReliableStateManager m_reliableStateManager;

        [NotNull]
        private string m_cacheName;

        public ReliableObjectCache(
            [NotNull] string cacheName,
            [NotNull] IReliableStateManager reliableStateManager)
        {
            m_cacheName = cacheName;
            m_reliableStateManager = reliableStateManager;
        }

        public async Task Add(string key, TValue value)
        {
            var dict = await GetOrAddDict();
            using (var tx = m_reliableStateManager.CreateTransaction())
            {
                await dict.AddAsync(tx, key, value);

                await tx.CommitAsync();
            }
        }

        public async Task Put(string key, TValue value)
        {
            var dict = await GetOrAddDict();
            using (var tx = m_reliableStateManager.CreateTransaction())
            {
                await dict.AddOrUpdateAsync(tx, key, existingKey => value, (existingKey, existingValue) => value);

                await tx.CommitAsync();
            }
        }

        [ItemCanBeNull]
        public async Task<TValue> GetItemOrDefault(string key)
        {
            var dict = await GetOrAddDict();
            using (var tx = m_reliableStateManager.CreateTransaction())
            {
                var result = await dict.TryGetValueAsync(tx, key);

                return result.HasValue ? result.Value : default;
            }
        }

        public async Task Remove(string key)
        {
	        var dict = await GetOrAddDict();

	        using (var tx = m_reliableStateManager.CreateTransaction())
	        {
		        await dict.TryRemoveAsync(tx, key);

		        await tx.CommitAsync();
	        }
        }

        [NotNull]
        private Task<IReliableDictionary<string, TValue>> GetOrAddDict()
        {
           return m_reliableStateManager.GetOrAddAsync<IReliableDictionary<string, TValue>>(m_cacheName);
        }
    }
}
