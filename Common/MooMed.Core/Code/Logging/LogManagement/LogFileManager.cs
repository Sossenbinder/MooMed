using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Retry;
using MooMed.Core.Code.Logging.LogManagement.Interface;

namespace MooMed.Core.Code.Logging.LogManagement
{
    internal class LogFileManager : IStartable
    {
        private const int _maxFileSizeBytes = 500000;

        private Timer _logFileOverflowChecker;

        [NotNull]
        private readonly CancellationTokenSource _cancellationTokenSource;

        [NotNull]
        private readonly IConfigSettingsProvider _settingsProvider;

        [NotNull]
        private readonly ILogPathProvider _logPathProvider;

        [NotNull]
        private FileInfo _logFileInfo;

        public LogFileManager(
            [NotNull] IConfigSettingsProvider settingsProvider,
            [NotNull] ILogPathProvider logPathProvider)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _settingsProvider = settingsProvider;
            _logPathProvider = logPathProvider;
        }

        public void Start()
        {
            var intervalTime = GetIntervalTimeForOverflowChecker();
            _logFileOverflowChecker = new Timer(
                async (state) => await CheckForLogFileOverflow(state, _cancellationTokenSource.Token),
                null,
                0,
                intervalTime);

            _logFileInfo = new FileInfo(_logPathProvider.GetMainLogPath());
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            // ReSharper disable once PossibleNullReferenceException
            _logFileOverflowChecker.Dispose();
        }

        private async Task CheckForLogFileOverflow(object state, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                if (!File.Exists(_logPathProvider.GetMainLogPath()))
                {
                    try
                    {
                        await RetryStrategy.DoRetry(CreateMainLog);
                    }
                    catch (AggregateException aggrExc)
                    {
                        // TODO: Handle this
                        throw aggrExc;
                    }
                }

                if (DoesLogFileOverflow())
                {
                    await HandleLogOverflow();
                }
            }
        }

        private void CreateMainLog()
        {
            var mainLogDir = _logPathProvider.GetMainLogDirectory();

            if (!Directory.Exists(mainLogDir))
            {
                Directory.CreateDirectory(mainLogDir);
            }

            File.Create(_logPathProvider.GetMainLogPath());
        }

        private bool DoesLogFileOverflow()
        {
            var fileSize = _logFileInfo.Length;

            return fileSize > _maxFileSizeBytes;
        }

        private async Task HandleLogOverflow()
        {
            var burialDirectory = _logPathProvider.GetBurialLogDirectory();

            if (Directory.Exists(burialDirectory))
            {
				await RetryStrategy.DoRetry(() => Directory.CreateDirectory(burialDirectory));
            }

            var burialPath = _logPathProvider.GetTimeStampedBurialPath();

            await RetryStrategy.DoRetry(() => File.Move(_logPathProvider.GetMainLogPath(), burialPath));
            
            await RetryStrategy.DoRetry(() => File.Create(_logPathProvider.GetMainLogPath()));
        }

        private int GetIntervalTimeForOverflowChecker()
        {
            var intervalForOverflowChecker = _settingsProvider.ReadValueOrFail<int>("intervalForOverflowCheckerInSeconds");

            return intervalForOverflowChecker * 1000;
        }
    }
}
