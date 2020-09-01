using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Module.Finance.Database
{
	internal class FinanceDbContextFactory : AbstractDbContextFactory<FinanceDbContext>
	{
		public FinanceDbContextFactory([NotNull] IConfigProvider configProvider)
			: base(configProvider, "MooMed_Db_Finance")
		{
		}

		public override FinanceDbContext CreateContext()
		{
			return new FinanceDbContext(GetConnectionString());
		}
	}
}