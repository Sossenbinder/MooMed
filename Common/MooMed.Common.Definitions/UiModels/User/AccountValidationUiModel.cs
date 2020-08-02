using System;
using System.Collections.Generic;
using System.Text;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.UiModels.User
{
	public class AccountValidationUiModel : IUiModel
	{
		public int AccountId { get; set; }

		public string Token { get; set; }
	}
}
