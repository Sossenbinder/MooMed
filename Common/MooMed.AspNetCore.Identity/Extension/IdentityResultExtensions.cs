using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MooMed.AspNetCore.Identity.DataTypes;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace MooMed.AspNetCore.Identity.Extension
{
	public static class IdentityResultExtensions
	{
		public static IdentityErrorCode? FirstErrorOrNull(this IdentityResult identityResult)
		{
			var errorCodes = identityResult
				.GetErrorCodes()
				.ToList();

			return errorCodes.Any() ? errorCodes.FirstOrDefault() : (IdentityErrorCode?)null;
		}

		public static IdentityErrorCode FirstErrorOrDefault(this IdentityResult identityResult)
		{
			var errorCode = identityResult.FirstErrorOrNull();

			return errorCode ?? IdentityErrorCode.DefaultError;
		}

		public static IEnumerable<IdentityErrorCode> GetErrorCodes(this IdentityResult identityResult)
		{
			var errorCodes = identityResult
				.Errors
				.OfType<CodeIdentityError>()
				.Select(x => x.ErrorCode);

			return errorCodes;
		}
	}
}