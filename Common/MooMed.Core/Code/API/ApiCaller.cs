using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.Code.API.Interface;
using MooMed.Core.Code.API.Types;
using MooMed.Core.Code.Extensions;
using Newtonsoft.Json;

namespace MooMed.Core.Code.API
{
	public class ApiCaller : IApiCaller
	{
		[NotNull]
		private readonly HttpClient _httpClient;

		public ApiCaller([NotNull] HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<TOut> PostWithJson<TIn, TOut>(PostData<TIn> postData)
		{
			var response = await _httpClient.PostAsJsonAsync(postData.Path, postData.Data);

			var responseStr = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TOut>(responseStr);
		}

		public async Task<IEnumerable<TOut>> PostWithJsonSequential<TIn, TOut>(
			PostData<TIn> postData, 
			Func<TOut, bool> retryDeterminerFunc,
			Action<PostData<TIn>> onSuccessTransformer,
			TimeSpan? waitTimer = null)
		{
			if (waitTimer == null)
			{
				waitTimer = TimeSpan.Zero;
			}

			var responseData = new List<TOut>();
			var currentPostData = postData;

			while (true)
			{
				var response = await PostWithJson<TIn, TOut>(currentPostData);

				responseData.Add(response);

				if (!retryDeterminerFunc(response))
				{
					break;
				}

				onSuccessTransformer(currentPostData);

				await Task.Delay(waitTimer.Value);
			}

			return responseData;
		}
	}
}
