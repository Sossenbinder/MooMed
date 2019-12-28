using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Caching.ReliableCollection.Interface
{
    public interface IReliableCache<TValue>
    {
        Task Add([NotNull] string key, [NotNull] TValue value);

        Task Put([NotNull] string key, [NotNull] TValue value);

        Task<TValue> GetItemOrDefault([NotNull] string key);

        Task Remove([NotNull] string key);
    }
}
