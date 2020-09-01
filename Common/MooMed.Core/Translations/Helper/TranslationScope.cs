using System;
using System.Globalization;
using System.Threading;
using MooMed.Common.Definitions;

namespace MooMed.Core.Translations.Helper
{
	public class TranslationScope : IDisposable
	{
		private readonly CultureInfo _previousCultureInfo;

		public TranslationScope(Language lang = Language.en)
		{
			_previousCultureInfo = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang.ToString());
		}

		public void Dispose()
		{
			Thread.CurrentThread.CurrentUICulture = _previousCultureInfo;
		}
	}
}