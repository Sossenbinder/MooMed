using System;

namespace MooMed.DotNet.Utils.ResetLazy
{
	/// <summary>
	/// Implementation of a resettable Lazy which also doesn't cache on
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ResetLazy<T> : BaseResetLazy<T>
	{
		private readonly Func<T> _factory;

		public ResetLazy(Func<T> factory)
		{
			_factory = factory;
		}

		public virtual T Value()
		{
			if (IsValueCreated)
			{
				return CachedValue;
			}

			try
			{
				CachedValue = _factory();
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