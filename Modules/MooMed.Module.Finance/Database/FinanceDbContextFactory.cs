using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;

namespace MooMed.Module.Finance.Database
{
	internal class FinanceDbContextFactory : AbstractDbContextFactory<FinanceDbContext>
	{
		public FinanceDbContextFactory([NotNull] IConfigSettingsProvider configSettingsProvider) 
			: base(configSettingsProvider, "MooMed_Db_Finance")
		{

		}

		public override FinanceDbContext CreateContext()
		{
			return new FinanceDbContext(GetConnectionString());
		}
	}
}