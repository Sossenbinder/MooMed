using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MooMed.ServiceFabric.Helper.Interface;

namespace MooMed.ServiceFabric.Helper
{
	public class ReliableCollectionHelper : IReliableTransactionPerformer
	{
		public async Task RunInTransactionAndCommit()
		{

		}

		public Task PerformAction()
		{
			throw new NotImplementedException();
		}
	}
}
