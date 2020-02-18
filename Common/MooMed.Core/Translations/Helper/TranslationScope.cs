using System;
using System.Globalization;
using System.Threading;
using MooMed.Common.Definitions;

namespace MooMed.Core.Translations.Helper
{
    public class TranslationScope : IDisposable
    {
        private readonly CultureInfo m_previousCultureInfo;

        public TranslationScope(Language lang = Language.en)
        {
            m_previousCultureInfo = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang.ToString());
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentUICulture = m_previousCultureInfo;
        }
    }
}
