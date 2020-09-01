using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface IAccountValidationService : IGrpcService
	{
		[OperationContract]
		[NotNull]
		Task<ServiceResponse<bool>> ValidateRegistration([NotNull] AccountValidationModel accountValidationModel);
	}
}