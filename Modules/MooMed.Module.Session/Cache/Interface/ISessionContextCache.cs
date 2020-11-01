using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Session.Cache.Interface
{
	public interface ISessionContextCache
	{
		public ValueTask PutItem([NotNull] ISessionContext sessionContext, [CanBeNull] int? secondsToLive = null);

		public ValueTask<ISessionContext> GetItem([NotNull] string key);

		public ValueTask RemoveItem([NotNull] ISessionContext sessionContext);
	}
}