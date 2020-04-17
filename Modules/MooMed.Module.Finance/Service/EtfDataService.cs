using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Autofac;
using MooMed.Module.Finance.Helper.Interface;
using MooMed.Module.Finance.Service.Interface;

namespace MooMed.Module.Finance.Service
{
	public class EtfDataService : IEtfDataService, IStartable
	{
		[NotNull]
		private readonly IEtfQuerier _etfQuerier;

		public EtfDataService([NotNull] IEtfQuerier etfQuerier)
		{
			_etfQuerier = etfQuerier;
		}

		public async Task GetEtfs()
		{
			await _etfQuerier.GetAllEtfs();
		}

		public void Start()
		{
			//Task.Run(GetEtfs);
		}
	}
}
