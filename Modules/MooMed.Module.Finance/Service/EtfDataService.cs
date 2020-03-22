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
		private readonly IEtfQuerier m_etfQuerier;

		public EtfDataService([NotNull] IEtfQuerier etfQuerier)
		{
			m_etfQuerier = etfQuerier;
		}

		public async Task GetEtfs()
		{
			await m_etfQuerier.GetAllEtfs();
		}

		public void Start()
		{
			//Task.Run(GetEtfs);
		}
	}
}
