using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;
using System.Threading.Tasks;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface IPersonalDataService
	{
		Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalDataModel);

		Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePasswordModel);
	}
}