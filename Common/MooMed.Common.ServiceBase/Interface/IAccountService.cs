using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.Definitions.Models.User;
using MooMed.Core.DataTypes;
using MooMed.Core.Translations;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
    public interface IAccountService : IGrpcService
    {
        [NotNull]
        Task<WorkerResponse<LoginResult>> Login([NotNull] LoginModel loginModel);

        [NotNull]
        Task RefreshLoginForAccount(int accountId);

        [NotNull]
        Task<RegistrationResult> Register([NotNull] RegisterModel registerModel, Language lang);

        [NotNull]
        Task LogOff([NotNull] ISessionContext sessionContext);

        [NotNull]
        Task<Account> FindById(int accountId);

        [NotNull]
        Task<List<Account>> FindAccountsStartingWithName([NotNull] string name);

        [CanBeNull]
        Task<Account> FindByEmail([NotNull] string email);
    }
}
