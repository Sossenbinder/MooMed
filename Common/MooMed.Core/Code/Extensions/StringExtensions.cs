using JetBrains.Annotations;

namespace MooMed.Core.Code.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty([CanBeNull] this string stringToCheck) => string.IsNullOrEmpty(stringToCheck);
    }
}
