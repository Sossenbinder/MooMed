using Microsoft.AspNetCore.Identity;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace MooMed.AspNetCore.Identity.DataTypes
{
	public class CodeIdentityError : IdentityError
	{
		public IdentityErrorCode ErrorCode { get; set; }
	}
}