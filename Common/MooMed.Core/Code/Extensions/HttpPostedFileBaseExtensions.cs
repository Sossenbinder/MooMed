using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace MooMed.Core.Code.Extensions
{
    public static class IFormFileExtensions
    {
        [NotNull]
        public static string GetFileExtension([NotNull] this IFormFile formFile, bool lowerCase = true)
        {
            var extension = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'));

            return lowerCase ? extension.ToLower() : extension;
        }
    }
}
