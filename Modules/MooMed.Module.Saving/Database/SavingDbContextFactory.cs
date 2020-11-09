using JetBrains.Annotations;
using MooMed.Common.Database.Context;
using MooMed.Common.Definitions.Configuration;

namespace MooMed.Module.Saving.Database
{
	public class SavingDbContextFactory : AbstractDbContextFactory<SavingDbContext>
	{
		public SavingDbContextFactory([NotNull] IConfigProvider configProvider)
			: base(configProvider, "MooMed_Db_Saving")
		{
		}

		public override SavingDbContext CreateContext()
		{
			return new SavingDbContext(GetConnectionString());
		}
	}
}