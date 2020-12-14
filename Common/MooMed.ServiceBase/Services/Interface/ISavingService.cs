using System.ServiceModel;
using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.Saving;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
    [ServiceContract]
    public interface ISavingService : IGrpcService
    {
        [OperationContract]
        Task<ServiceResponse> SetCurrency(SetCurrencyModel setCurrencyModel);

        [OperationContract]
        Task<ServiceResponse> SetAssets(AssetsModel assetsModel);

        [OperationContract]
        Task<ServiceResponse> SetCashFlowItems(SetCashFlowItemsModel setCashFlowItemsModel);

        [OperationContract]
        Task<ServiceResponse<SavingInfoModel>> GetSavingInformation(ISessionContext sessionContext);
    }
}