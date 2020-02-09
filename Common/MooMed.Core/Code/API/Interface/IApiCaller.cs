using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.Code.API.Types;

namespace MooMed.Core.Code.API.Interface
{
	public interface IApiCaller
	{
		Task<TOut> PostWithJson<TIn, TOut>([System.Diagnostics.CodeAnalysis.NotNull] PostData<TIn> postData);

		Task<IEnumerable<TOut>> PostWithJsonSequential<TIn, TOut>(
			[NotNull] PostData<TIn> postData, 
			[NotNull] Func<TOut, bool> retryDeterminerFunc, 
			[NotNull] Action<PostData<TIn>> onSuccessTransformer,
			[CanBeNull] TimeSpan? waitTimer = null);
	}
}
