using MooMed.Common.Database.Context;

namespace MooMed.Module.Finance.Database
{
	class FinanceDbContext : AbstractDbContext
	{
		public FinanceDbContext(string connectionString) 
			: base(connectionString)
		{
		}
	}
}
