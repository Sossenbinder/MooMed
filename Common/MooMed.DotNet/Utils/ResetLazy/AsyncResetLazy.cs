using System;
using System.Threading.Tasks;

namespace MooMed.DotNet.Utils.ResetLazy
{
	public class AsyncResetLazy<T> : BaseResetLazy<T>
	{
		private readonly Func<Task<T>> _factory;

		public AsyncResetLazy(Func<Task<T>> factory)
		{
			_factory = factory;
		}

		public async ValueTask<T> Value()
		{
			if (IsValueCreated)
			{
				return CachedValue;
			}

			try
			{
				CachedValue = await _factory();
			}
			catch (Exception)
			{
				IsValueCreated = false;
				throw;
			}

			IsValueCreated = true;

			return CachedValue;
		}
	}
}