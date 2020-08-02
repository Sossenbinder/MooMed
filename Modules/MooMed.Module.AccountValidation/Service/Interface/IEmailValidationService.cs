using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.AccountValidation.Service.Interface
{
	public interface IEmailValidationService
	{
		Task ValidateAccount(AccountValidationModel accountValidationModel);
	}
}
