using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MooMed.ServiceFabric.Helper.Interface
{
	interface IReliableTransactionPerformer
	{
		Task PerformAction();
	}
}
