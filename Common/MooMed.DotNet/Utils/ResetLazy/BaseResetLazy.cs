namespace MooMed.DotNet.Utils.ResetLazy
{
	public class BaseResetLazy<T>
	{
		public bool IsValueCreated { get; protected set; }

		protected T CachedValue;

		public void Reset()
		{
			IsValueCreated = false;
		}
	}
}