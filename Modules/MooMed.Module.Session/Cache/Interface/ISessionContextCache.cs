using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Module.Session.Cache.Interface
{
	public interface ISessionContextCache
	{
		public void PutItem(string key, ISessionContext value, int? secondsToLive = null);

		public ISessionContext GetItem(string key);
	}
}
