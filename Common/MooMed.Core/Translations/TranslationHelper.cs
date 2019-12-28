using System;
using JetBrains.Annotations;

namespace MooMed.Core.Translations
{
    internal class FormatException : Exception
    {
        public FormatException([NotNull] string message)
            : base(message)
        {
        }
    }

    public static class TranslationFormatter
    {
        [NotNull]
        public static string FormatWithParams([NotNull] string translation, [NotNull] params string[] replaceParams)
        {
            var formattedString = translation;

            for (var i = 0; i < replaceParams.Length; ++i)
            {
                var formatReplacee = $"{{{{{i}}}}}";

                if (formattedString.Contains(formatReplacee))
                {
                    formattedString = formattedString.Replace(formatReplacee, replaceParams[i]);
                }
                else
                {
                    throw new FormatException($"Could not find formattee with index {i} for param {replaceParams[i]}");
                }
            }

            return formattedString;
        }
    }
}
