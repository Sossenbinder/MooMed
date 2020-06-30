using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Module.Portfolio.Database
{
	internal class PortfolioDbContextFactory : AbstractDbContextFactory<PortfolioDbContext>
	{
		public PortfolioDbContextFactory([NotNull] IConfigSettingsProvider configSettingsProvider)
			: base(configSettingsProvider, "MooMed_Database_Portfolio")
		{

		}

		public override PortfolioDbContext CreateContext()
		{
			return new PortfolioDbContext(GetConnectionString());
		}
	}
}
