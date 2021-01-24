using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MooMed.DotNet.Extensions
{
	public static class CancellationTokenAsyncExtensions
	{
		public static CancellationTokenAwaiter GetAwaiter(this CancellationToken ct)
		{
			return new(ct);
		}

		public readonly struct CancellationTokenAwaiter : ICriticalNotifyCompletion
		{
			private readonly CancellationToken _cancellationToken;

			public CancellationTokenAwaiter(CancellationToken cancellationToken)
			{
				_cancellationToken = cancellationToken;
			}

			public object GetResult()
			{
				if (IsCompleted)
				{
					throw new OperationCanceledException();
				}

				throw new InvalidOperationException("The cancellation token has not yet been cancelled.");
			}

			public bool IsCompleted => _cancellationToken.IsCancellationRequested;

			public void OnCompleted(Action continuation) => _cancellationToken.Register(continuation);

			public void UnsafeOnCompleted(Action continuation) => _cancellationToken.Register(continuation);
		}
	}
}