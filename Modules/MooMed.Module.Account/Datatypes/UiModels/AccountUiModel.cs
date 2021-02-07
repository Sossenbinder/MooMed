using MooMed.Common.Definitions.Interface;

namespace MooMed.Module.Accounts.Datatypes.UiModels
{
	public class AccountUiModel : IUiModel
	{
		public int Id { get; set; }

		public string UserName { get; set; } = null!;

		public string Email { get; set; } = null!;
	}
}