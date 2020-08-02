using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MooMed.AspNetCore.Identity.DataTypes;
using MooMed.Common.Definitions.Models.User.ErrorCodes;

namespace MooMed.AspNetCore.Identity.Helper
{
	public class CustomDuplicateEmailUserValidator<T> : IUserValidator<T>
		where T : IdentityUser<int>
	{
		public async Task<IdentityResult> ValidateAsync(UserManager<T> manager, T user)
		{
			var combinationExists = await manager.Users
				.AnyAsync(x => x.Email.Equals(user.Email));

			if (combinationExists)
			{
				return IdentityResult.Failed(new CodeIdentityError()
				{
					ErrorCode = IdentityErrorCode.DuplicateEmail,
				});
			}

			return IdentityResult.Success;
		}
	}
}