using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MooMed.Module.Finance.DataTypes;

namespace MooMed.Module.Finance.Helper.Interface
{
	public interface IEtfQuerier
	{
		Task<List<EtfMetadata>> GetAllEtfs();
	}
}
