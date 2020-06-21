using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Core.DataTypes;

namespace MooMed.Module.Finance.Service.Interface
{
	public interface IExchangeTradedsService
	{
		Task<ServiceResponse<IEnumerable<ExchangeTradedModel>>> GetExchangeTradeds();
	}
}
