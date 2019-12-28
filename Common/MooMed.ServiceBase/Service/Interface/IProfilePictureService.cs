using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Service.Interface.Base;

namespace MooMed.ServiceBase.Service.Interface
{
	public interface IProfilePictureServiceProxy : IProfilePictureService
	{

	}

	[ServiceKnownType(typeof(SessionContext))]
	public interface IProfilePictureService : IRemotingServiceBase
	{
        Task<bool> ProcessUploadedProfilePicture([NotNull] ISessionContext sessionContext, [NotNull] ProfilePictureData pictureData);

		[ItemNotNull]
        Task<string> GetProfilePictureForAccountById(int accountId);

        [ItemNotNull]
        Task<string> GetProfilePictureForAccount(SessionContext sessionContext);
    }
}