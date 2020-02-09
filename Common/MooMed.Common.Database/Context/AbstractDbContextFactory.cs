using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Database.Context.Interface;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Common.Database.Context
{
	public abstract class AbstractDbContextFactory<TDbContext> : IDbContextFactory<TDbContext> 
		where TDbContext : AbstractDbContext
	{
		[NotNull]
		private readonly IConfigSettingsProvider m_configSettingsProvider;

		[NotNull]
		private readonly string m_key;

		protected AbstractDbContextFactory([NotNull] IConfigSettingsProvider configSettingsProvider, [NotNull] string key)
		{
			m_configSettingsProvider = configSettingsProvider;
			m_key = key;
		}

		protected string GetConnectionString()
		{
			return m_configSettingsProvider.ReadDecryptedValueOrFail<string>(m_key, "Password");
		}

		public abstract TDbContext CreateContext();
	}
}
