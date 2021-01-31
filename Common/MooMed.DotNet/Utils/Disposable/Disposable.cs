using MooMed.DotNet.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MooMed.DotNet.Utils.Disposable
{
	public class Disposable : IDisposable, IAsyncDisposable
	{
		private readonly List<IDisposable> _disposables;

		private readonly List<IAsyncDisposable> _asyncDisposables;

		public Disposable()
		{
			_disposables = new List<IDisposable>();
			_asyncDisposables = new List<IAsyncDisposable>();
		}

		protected void RegisterDisposable(IDisposable disposable) => _disposables.Add(disposable);

		protected void RegisterAsyncDisposable(IAsyncDisposable asyncDisposable) => _asyncDisposables.Add(asyncDisposable);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			foreach (var disposable in _disposables)
			{
				disposable.Dispose();
			}
		}

		public ValueTask DisposeAsync()
		{
			GC.SuppressFinalize(this);
			return DisposeAsync(true);
		}

		protected virtual async ValueTask DisposeAsync(bool disposing)
		{
			if (!disposing)
			{
				return;
			}

			await _asyncDisposables.ParallelAsyncValueTask(x => x.DisposeAsync());

			Dispose(true);
		}
	}
}