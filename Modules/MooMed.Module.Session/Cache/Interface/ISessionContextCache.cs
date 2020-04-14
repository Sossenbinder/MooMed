using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Session.Cache.Interface
{
	public interface ISessionContextCache
	{
		public void PutItem([NotNull] ISessionContext sessionContext, [CanBeNull] int? secondsToLive = null);

		public ISessionContext GetItem([NotNull] string key);

		public void RemoveItem([NotNull] ISessionContext sessionContext);
	}
}
