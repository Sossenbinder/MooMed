using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Retry
{
    public static class RetryStrategy
    {
        public static async Task DoRetry([NotNull] Action action, [CanBeNull] TimeSpan? msBetweenRetries = null, int retryCount = 10)
        {
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

                if (msBetweenRetries.HasValue)
                {
                    await Task.Delay(msBetweenRetries.Value);
                }
            }

            throw new AggregateException(subExceptions);

        }
    }
}
