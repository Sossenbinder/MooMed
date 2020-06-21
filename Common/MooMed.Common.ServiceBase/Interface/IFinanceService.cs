using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Finance;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IFinanceService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<IEnumerable<ExchangeTradedModel>>> GetExchangeTradeds();

	}
}
