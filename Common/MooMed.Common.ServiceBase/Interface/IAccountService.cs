using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations;
using MooMed.Grpc.Definitions.Interface;
using ProtoBuf.Grpc;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
    public interface IAccountService : IGrpcService
    {
        [OperationContract]
        [NotNull]
        Task<ServiceResponse<LoginResult>> Login([NotNull] LoginModel loginModel);

        [OperationContract]
        [NotNull]
        Task RefreshLoginForAccount([NotNull] Primitive<int> accountId);

        [OperationContract]
        [NotNull]
        Task<RegistrationResult> Register([NotNull] RegisterModel registerModel);

        [OperationContract]
        [NotNull]
        Task LogOff([NotNull] ISessionContext sessionContext);

        [OperationContract]
        [NotNull]
        Task<Account> FindById([NotNull] Primitive<int> accountId);

        [OperationContract]
        [NotNull]
        Task<List<Account>> FindAccountsStartingWithName([NotNull] string name);

        [OperationContract]
        [CanBeNull]
        Task<Account> FindByEmail([NotNull] string email);
    }
}
