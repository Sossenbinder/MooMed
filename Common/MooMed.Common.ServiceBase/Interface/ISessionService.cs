using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface ISessionService : IGrpcService
	{
        [OperationContract]
        Task<ServiceResponse<ISessionContext>> GetSessionContext([NotNull] AccountIdQuery accountIdQuery);

        [OperationContract]
        Task<ISessionContext> LoginAccount([NotNull] Account account);
    }
}
 