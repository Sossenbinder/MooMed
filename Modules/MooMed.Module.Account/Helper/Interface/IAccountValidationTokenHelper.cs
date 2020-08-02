using JetBrains.Annotations;

namespace MooMed.Module.Accounts.Helper.Interface
{
	public interface IAccountValidationTokenHelper
	{
		string Serialize([NotNull] string rawToken);

		string Deserialize(string encodedToken);
	}
}