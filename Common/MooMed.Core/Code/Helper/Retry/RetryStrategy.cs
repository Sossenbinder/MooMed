using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Retry
{
    public static class RetryStrategy
    {
        public static Task DoRetry(
	        [NotNull] Action action,
	        [CanBeNull] Func<int, Task> onRetry = null,
			int msBetweenRetries = 0,
	        int retryCount = 10)
        {
	        return RetryInternal(action, msBetweenRetries, retryCount, onRetry);
        }

        public static Task DoRetryExponential(
	        [NotNull] Action action,
	        [CanBeNull] Func<int, Task> onRetry = null,
			int msBetweenRetries = 1000,
			int retryScalingFactor = 2,
	        int retryCount = 10)
		{
			return RetryInternal(action, msBetweenRetries, retryCount, onRetry, currentWaitTime => currentWaitTime * retryScalingFactor);
		}

        private static async Task RetryInternal(
	        [NotNull] Action action,
	        int msBetweenRetries,
			int retryCount,
	        [CanBeNull] Func<int, Task> onRetry = null,
	        [CanBeNull] Func<int, int> waitTimeTransformer = null)
        {
	        var msUntilNextRetry = msBetweenRetries;
			var subExceptions = new List<Exception>();

			for (var i = 0; i < retryCount; ++i)
			{
				try
				{
					action();
					return;
				}
				catch (Exception exc)
				{
					subExceptions.Add(exc);
				}

				if (msBetweenRetries > 0)
				{
					await Task.Delay(msUntilNextRetry);
					msBetweenRetries = waitTimeTransformer(msBetweenRetries);
				}

				if (onRetry != null)
				{
					await onRetry(i);
				}
			}

			throw new AggregateException(subExceptions);
		}
    }
}
