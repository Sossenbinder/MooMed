using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface ILoginService
	{
		[NotNull]
		Task<LoginResult> Login([NotNull] LoginModel loginModel);

		[NotNull]
		Task<bool> RefreshLastAccessed([NotNull] ISessionContext sessionContext);
	}
}
