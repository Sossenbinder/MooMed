using System;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Html;
using MooMed.Common.Definitions;

namespace MooMed.Frontend.Views
{
	public static class MooMedHtmlHelper
	{
		[NotNull]
		public static HtmlString TranslationFile(Language lang)
		{
			string langString;

			if (lang != Language.none)
			{
				langString = Enum.GetName(typeof(Language), lang) ?? Language.en.ToString();
			}
			else
			{
				var currentThreadCulture = Thread.CurrentThread.CurrentUICulture;
				langString = currentThreadCulture.Name;
			}

			return new HtmlString($"<script type=\"text/javascript\" src=\"/dist/Translations/translation.{langString}.js\"></script>");
		}

		[NotNull]
		private static HtmlString AttachToWindowObject([NotNull] string name, [NotNull] object value)
		{
			return new($"window.{name} = {value}");
		}
	}
}