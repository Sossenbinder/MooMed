using System.ServiceModel;
using Microsoft.ServiceFabric.Services.Remoting;
using MooMed.Common.Definitions.Models.Session;

namespace MooMed.ServiceBase.Service.Interface.Base
{
	[ServiceKnownType(typeof(SessionContext))]
	public interface IRemotingServiceBase : IService
	{
	}
}
