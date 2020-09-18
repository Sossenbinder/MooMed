using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.User
{
	public class PersonalDataUiModel : IUiModel
	{
		public string? Email { get; set; }

		public string? UserName { get; set; }

		public string? Password { get; set; }
	}
}