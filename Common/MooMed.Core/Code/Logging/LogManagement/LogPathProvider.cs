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
        private readonly string _baseLogDir;

        [NotNull]
        private readonly string _mainLogPath;

        [NotNull]
        private readonly string _burialLogDir;

        [NotNull]
        private readonly string _logFileName;

        public LogPathProvider(
            [NotNull] IConfigSettingsProvider settingsProvider)
        {
            _logFileName = settingsProvider.ReadValueOrFail<string>("MooMed_LogFileName");

            _baseLogDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MooMed");
            _mainLogPath = $"{Path.Combine(_baseLogDir, _logFileName)}.txt";
            _burialLogDir = Path.Combine(_baseLogDir, "BuriedLogs");
        }

        public string GetMainLogDirectory() => _baseLogDir;

        public string GetMainLogPath() => _mainLogPath;

        public string GetBurialLogDirectory() => _burialLogDir;

        public string GetTimeStampedBurialPath()
        {
            var timeStamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture).Replace(" ", "-");

            var timeStampedFileName = $"{_logFileName}_{timeStamp}.txt";

            return Path.Combine(_burialLogDir, timeStampedFileName);
        }
    }
}
