namespace MooMed.DotNet.Utils.ResetLazy
{
    public abstract class BaseResetLazy<T>
    {
        public bool IsValueCreated { get; protected set; }

        protected T CachedValue = default!;

        public void Reset()
        {
            IsValueCreated = false;
        }
    }
}