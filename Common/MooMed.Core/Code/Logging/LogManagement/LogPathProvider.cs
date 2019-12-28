using System;
using System.Globalization;
using System.IO;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Logging.LogManagement.Interface;

namespace MooMed.Core.Code.Logging.LogManagement
{
    public class LogPathProvider : ILogPathProvider
    {
        [NotNull]
        private readonly string m_baseLogDir;

        [NotNull]
        private readonly string m_mainLogPath;

        [NotNull]
        private readonly string m_burialLogDir;

        [NotNull]
        private readonly string m_logFileName;

        public LogPathProvider(
            [NotNull] IConfigSettingsProvider settingsProvider)
        {
            m_logFileName = settingsProvider.ReadValueOrFail<string>("MooMed_LogFileName");

            m_baseLogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MooMed");
            m_mainLogPath = $"{Path.Combine(m_baseLogDir, m_logFileName)}.txt";
            m_burialLogDir = Path.Combine(m_baseLogDir, "BuriedLogs");
        }

        public string GetMainLogDirectory() => m_baseLogDir;

        public string GetMainLogPath() => m_mainLogPath;

        public string GetBurialLogDirectory() => m_burialLogDir;

        public string GetTimeStampedBurialPath()
        {
            var timeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture).Replace(" ", "-");

            var timeStampedFileName = $"{m_logFileName}_{timeStamp}.txt";

            return Path.Combine(m_burialLogDir, timeStampedFileName);
        }
    }
}
