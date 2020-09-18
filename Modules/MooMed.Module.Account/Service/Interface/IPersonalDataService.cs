using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.User;
using MooMed.Common.Definitions.Models.User.ErrorCodes;
using MooMed.Core.DataTypes;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface IPersonalDataService
	{
		Task<ServiceResponse<IdentityErrorCode>> UpdatePersonalData(PersonalData personalData);

		Task<ServiceResponse<IdentityErrorCode>> UpdatePassword(UpdatePassword updatePassword);
	}
}