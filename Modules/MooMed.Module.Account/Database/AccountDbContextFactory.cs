﻿using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Module.Accounts.Database
{
	public class AccountDbContextFactory : AbstractDbContextFactory<AccountDbContext>
	{
		public AccountDbContextFactory([NotNull] IConfigSettingsProvider configSettingsProvider, [NotNull] string key)
			: base(configSettingsProvider, key)
		{

		}

		public override AccountDbContext CreateContext()
		{
			return new AccountDbContext(GetConnectionString());
		}
	}
}