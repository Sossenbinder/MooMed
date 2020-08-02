using System.Threading.Tasks;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface IAccountOnlineStateService
	{
		Task<AccountOnlineState> GetOnlineStateForAccount(int accountId);
	}
}