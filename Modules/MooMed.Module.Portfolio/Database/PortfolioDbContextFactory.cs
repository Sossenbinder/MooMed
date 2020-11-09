using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Module.Portfolio.Database
{
	internal class PortfolioDbContextFactory : AbstractDbContextFactory<PortfolioDbContext>
	{
		public PortfolioDbContextFactory([NotNull] IConfigProvider configProvider)
			: base(configProvider, "MooMed_Db_Main")
		{
		}

		public override PortfolioDbContext CreateContext()
		{
			return new PortfolioDbContext(GetConnectionString());
		}
	}
}