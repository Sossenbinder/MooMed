using Microsoft.AspNetCore.Http;

namespace MooMed.DotNet.Extensions
{
	public static class IFormFileExtensions
	{
		public static string GetFileExtension(this IFormFile formFile, bool lowerCase = true)
		{
			var extension = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.') + 1);

			return lowerCase ? extension.ToLower() : extension;
		}
	}
}