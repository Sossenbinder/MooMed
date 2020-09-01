using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MooMed.Common.Database.Context.Interface;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Common.Database.Context
{
	public abstract class AbstractDbContextFactory<TDbContext> : IDbContextFactory<TDbContext>
		where TDbContext : DbContext
	{
		[NotNull]
		private readonly IConfigProvider _configProvider;

		[NotNull]
		private readonly string _key;

		protected AbstractDbContextFactory([NotNull] IConfigProvider configProvider, [NotNull] string key)
		{
			_configProvider = configProvider;
			_key = key;
		}

		protected string GetConnectionString()
		{
			return _configProvider.ReadDecryptedValueOrFail<string>(_key, "Password");
		}

		public abstract TDbContext CreateContext();
	}
}