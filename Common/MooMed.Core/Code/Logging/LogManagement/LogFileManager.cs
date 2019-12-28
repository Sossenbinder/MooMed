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
        private const int m_maxFileSizeBytes = 500000;

        private Timer m_logFileOverflowChecker;

        [NotNull]
        private readonly CancellationTokenSource m_cancellationTokenSource;

        [NotNull]
        private readonly IConfigSettingsProvider m_settingsProvider;

        [NotNull]
        private readonly ILogPathProvider m_logPathProvider;

        [NotNull]
        private FileInfo m_logFileInfo;

        public LogFileManager(
            [NotNull] IConfigSettingsProvider settingsProvider,
            [NotNull] ILogPathProvider logPathProvider)
        {
            m_cancellationTokenSource = new CancellationTokenSource();

            m_settingsProvider = settingsProvider;
            m_logPathProvider = logPathProvider;
        }

        public void Start()
        {
            var intervalTime = GetIntervalTimeForOverflowChecker();
            m_logFileOverflowChecker = new Timer(
                async (state) => await CheckForLogFileOverflow(state, m_cancellationTokenSource.Token),
                null,
                0,
                intervalTime);

            m_logFileInfo = new FileInfo(m_logPathProvider.GetMainLogPath());
        }

        public void Stop()
        {
            m_cancellationTokenSource.Cancel();
            // ReSharper disable once PossibleNullReferenceException
            m_logFileOverflowChecker.Dispose();
        }

        private async Task CheckForLogFileOverflow(object state, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                if (!File.Exists(m_logPathProvider.GetMainLogPath()))
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
            var mainLogDir = m_logPathProvider.GetMainLogDirectory();

            if (!Directory.Exists(mainLogDir))
            {
                Directory.CreateDirectory(mainLogDir);
            }

            File.Create(m_logPathProvider.GetMainLogPath());
        }

        private bool DoesLogFileOverflow()
        {
            var fileSize = m_logFileInfo.Length;

            return fileSize > m_maxFileSizeBytes;
        }

        private async Task HandleLogOverflow()
        {
            var burialDirectory = m_logPathProvider.GetBurialLogDirectory();

            if (Directory.Exists(burialDirectory))
            {
				await RetryStrategy.DoRetry(() => Directory.CreateDirectory(burialDirectory));
            }

            var burialPath = m_logPathProvider.GetTimeStampedBurialPath();

            await RetryStrategy.DoRetry(() => File.Move(m_logPathProvider.GetMainLogPath(), burialPath));
            
            await RetryStrategy.DoRetry(() => File.Create(m_logPathProvider.GetMainLogPath()));
        }

        private int GetIntervalTimeForOverflowChecker()
        {
            var intervalForOverflowChecker = m_settingsProvider.ReadValueOrFail<int>("intervalForOverflowCheckerInSeconds");

            return intervalForOverflowChecker * 1000;
        }
    }
}
