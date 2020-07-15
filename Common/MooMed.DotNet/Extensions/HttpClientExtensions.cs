using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MooMed.DotNet.Extensions
{
	public static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> PostAsJsonAsync([NotNull] this HttpClient httpClient, [NotNull] string path, [NotNull] object obj)
		{
			return httpClient.PostAsync(path, new StringContent(JsonConvert.SerializeObject(obj)));
		}
	}
}
