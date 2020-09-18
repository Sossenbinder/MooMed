using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace MooMed.Module.AccountValidation.Service.Interface
{
	public interface IEmailValidationService
	{
		Task<IdentityErrorCode> ValidateAccount(AccountValidationModel accountValidationModel);
	}
}