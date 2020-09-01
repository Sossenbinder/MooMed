using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;

namespace MooMed.Module.Portfolio.Database
{
	internal class PortfolioDbContextFactory : AbstractDbContextFactory<PortfolioDbContext>
	{
		public PortfolioDbContextFactory([NotNull] IConfigProvider configProvider)
			: base(configProvider, "MooMed_Database_Portfolio")
		{
		}

		public override PortfolioDbContext CreateContext()
		{
			return new PortfolioDbContext(GetConnectionString());
		}
	}
}