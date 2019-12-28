using System;
using System.Collections.Generic;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace FrontendEnumGenerator
{
	public static class EnumsToExport
	{
		public static readonly List<Type> Enums = new List<Type>()
		{
			typeof(LoginResponseCode),
			typeof(AccountValidationResult),
		};
	}
}
