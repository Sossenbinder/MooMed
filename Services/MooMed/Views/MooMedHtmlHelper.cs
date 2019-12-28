using System;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Html;
using MooMed.Core.Translations;

namespace MooMed.Web.Views
{
    public static class MooMedHtmlHelper
    {
        [NotNull]
        public static HtmlString AddReactRouterRoute([NotNull] string route)
        {
            return AttachToWindowObject("reactRoute", route);
        }

        [NotNull]
        public static HtmlString TranslationFile(Language lang)
        {
            string langString;

            if (lang != Language.none)
            {
                langString = Enum.GetName(typeof(Language), lang);
            }
            else
            {
                var currentThreadCulture = Thread.CurrentThread.CurrentUICulture;
                langString = currentThreadCulture.Name;
            }

            return new HtmlString($"<script type=\"text/javascript\" src=\"dist/Translations/translation.{langString}.js\"></script>");
        }

        [NotNull]
        private static HtmlString AttachToWindowObject([NotNull] string name, [NotNull] object value)
        {
            return new HtmlString($"window.{name} = {value}");
        }
    }
}