﻿using System.Diagnostics.CodeAnalysis;
using MooMed.Common.Database.Context.Interface;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;

namespace MooMed.Common.Database.Context
{
	public abstract class AbstractDbContextFactory<TDbContext> : IDbContextFactory<TDbContext> 
		where TDbContext : AbstractDbContext
	{
		[NotNull]
		private readonly IConfigSettingsProvider _configSettingsProvider;

		[NotNull]
		private readonly string _key;

		protected AbstractDbContextFactory([NotNull] IConfigSettingsProvider configSettingsProvider, [NotNull] string key)
		{
			_configSettingsProvider = configSettingsProvider;
			_key = key;
		}

		protected string GetConnectionString()
		{
			return _configSettingsProvider.ReadDecryptedValueOrFail<string>(_key, "Password");
		}

		public abstract TDbContext CreateContext();
	}
}
