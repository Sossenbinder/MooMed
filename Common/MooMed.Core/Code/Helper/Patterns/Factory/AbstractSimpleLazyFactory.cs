using System;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Patterns.Factory
{
	public abstract class AbstractSimpleLazyFactory<T>
	{
		[NotNull]
		private readonly Lazy<T> m_valLazy;

		protected AbstractSimpleLazyFactory([NotNull] Func<T> valFunc)
		{
			m_valLazy = new Lazy<T>(valFunc);
		}

		public T Get() => m_valLazy.Value;
	}
}
