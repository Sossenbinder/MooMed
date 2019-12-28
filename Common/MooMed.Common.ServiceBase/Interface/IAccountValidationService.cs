using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	public interface IAccountValidationService : IGrpcService
    {
		[NotNull]
        Task<AccountValidationTokenData> DeserializeRawToken([NotNull] string token);

        [NotNull]
		Task<WorkerResponse<bool>> ValidateRegistration([NotNull] AccountValidationTokenData tokenData);
    }
}
