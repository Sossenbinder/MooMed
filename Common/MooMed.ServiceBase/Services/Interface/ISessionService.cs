using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface ISessionService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<ISessionContext>> GetSessionContext([NotNull] Primitive<int> accountId);

		[OperationContract]
		Task<ISessionContext> LoginAccount([NotNull] Account account);

		[OperationContract]
		[NotNull]
		Task UpdateSessionContext([NotNull] ISessionContext sessionContext);
	}
}