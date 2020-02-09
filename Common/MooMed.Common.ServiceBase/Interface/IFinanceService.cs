using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IFinanceService : IGrpcService
	{
	}
}
