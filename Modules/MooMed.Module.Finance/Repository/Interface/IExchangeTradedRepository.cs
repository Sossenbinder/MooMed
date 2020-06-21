using MooMed.Common.Database.Repository.Interface;
using MooMed.Module.Finance.Database.Entities;

namespace MooMed.Module.Finance.Repository.Interface
{
	public interface IExchangeTradedRepository : ICrudRepository<ExchangeTradedEntity, string>
	{
	}
}
