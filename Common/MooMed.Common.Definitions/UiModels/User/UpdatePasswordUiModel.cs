using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.User
{
	public class UpdatePasswordUiModel : IUiModel
	{
		public string OldPassword { get; set; }

		public string NewPassword { get; set; }
	}
}