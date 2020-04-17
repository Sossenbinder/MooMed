using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MooMed.Core.Code.API;
using MooMed.Core.Code.API.Interface;
using MooMed.Core.Code.API.Types;
using MooMed.Core.Code.Extensions;
using MooMed.Module.Finance.DataTypes;
using MooMed.Module.Finance.DataTypes.Json;
using MooMed.Module.Finance.Helper.Interface;
using Newtonsoft.Json;

namespace MooMed.Module.Finance.Helper
{
	public class EtfDbQuerier : IEtfQuerier
	{
		[NotNull]
		private const string _baseUri = "https://etfdb.com/api/screener/";

		[NotNull]
		private readonly IApiCaller _apiCaller;

		public EtfDbQuerier([NotNull] IHttpClientFactory httpClientFactory)
		{
			var httpClient = httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri(_baseUri);

			_apiCaller = new ApiCaller(httpClient);
		}

		public async Task<List<EtfMetadata>> GetAllEtfs()
		{
			var etfMetaData = new List<EtfMetadata>();

			var postData = new PostData<EtfDbQuery>("", new EtfDbQuery()
			{
				page = 1,
				per_page = 1000,
				structure = new[] {"ETF"},
				only = new[] {"meta"},
			});

			//var response = await _apiCaller.PostWithJson<EtfDbQuery, RootObject>(postData);

			var responseBatch = await _apiCaller.PostWithJsonSequential<EtfDbQuery, RootObject>(
				postData,
				rootObj => rootObj.data.Any(),
				toTransform => toTransform.Data.page++,
				TimeSpan.FromMinutes(1));




			return etfMetaData;
		}
	}
}
