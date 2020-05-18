using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Module.Accounts.Service.Interface
{
	public interface IRegisterService
	{
		[NotNull]
		Task<RegistrationResult> Register([NotNull] RegisterModel registerModel);
	}
}
