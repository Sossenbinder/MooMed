using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Core.Code.Configuration.Interface;

namespace MooMed.Module.Finance.Database
{
	internal class FinanceDbContextFactory : AbstractDbContextFactory<FinanceDbContext>
	{
		protected FinanceDbContextFactory([NotNull] IConfigSettingsProvider configSettingsProvider, [NotNull] string key) 
			: base(configSettingsProvider, key)
		{

		}

		public override FinanceDbContext CreateContext()
		{
			return new FinanceDbContext(GetConnectionString());
		}
	}
}