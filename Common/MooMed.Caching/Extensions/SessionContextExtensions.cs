using JetBrains.Annotations;
using MooMed.Caching.Helper;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Caching.Extensions
{
	public static class SessionContextExtensions
	{
		public static string GetAccountKey([NotNull] this ISessionContext sessionContext) =>
			CacheKeyUtils.GetCacheKeyForAccountId(sessionContext.Account.Id);
	}
}
