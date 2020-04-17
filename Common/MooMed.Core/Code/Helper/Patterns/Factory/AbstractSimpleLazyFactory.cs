using System;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Patterns.Factory
{
	public abstract class AbstractSimpleLazyFactory<T>
	{
		[NotNull]
		private readonly Lazy<T> _valLazy;

		protected AbstractSimpleLazyFactory([NotNull] Func<T> valFunc)
		{
			_valLazy = new Lazy<T>(valFunc);
		}

		public T Get() => _valLazy.Value;
	}
}
