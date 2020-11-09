using System.ServiceModel;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface ISavingService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse> SetCurrency(SetCurrencyModel setCurrencyModel);
	}
}