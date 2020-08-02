using Microsoft.AspNetCore.Identity;

namespace MooMed.AspNetCore.Identity.Helper
{
	public static class IdentityOptionsProvider
	{
		public static void ApplyDefaultOptions(IdentityOptions options)
		{
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;

			options.SignIn.RequireConfirmedEmail = true;
		}
	}
}