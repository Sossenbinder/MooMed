using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IAccountValidationService : IGrpcService
	{
		[OperationContract]
		Task SendAccountValidationMail(AccountValidationMailData accountValidationMailData);

        [OperationContract]
        [NotNull]
        Task<AccountValidationTokenData> DeserializeRawToken([NotNull] string token);

        [OperationContract]
        [NotNull]
		Task<ServiceResponse<bool>> ValidateRegistration([NotNull] AccountValidationTokenData tokenData);
    }
}
