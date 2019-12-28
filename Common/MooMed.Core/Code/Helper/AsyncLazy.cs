using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper
{
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        public AsyncLazy([NotNull] Func<T> valueFactory)
	        : base(() => Task.Run(valueFactory))
        {

        }

        public AsyncLazy([NotNull] Func<Task<T>> taskFactory) 
	        : base(taskFactory)
        {

        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return Value.GetAwaiter();
        }
    }
}
