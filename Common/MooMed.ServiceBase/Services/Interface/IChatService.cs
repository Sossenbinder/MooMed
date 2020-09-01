using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Core.DataTypes;
using MooMed.ServiceBase.Definitions.Interface;

namespace MooMed.ServiceBase.Services.Interface
{
	[ServiceContract]
	public interface IChatService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<RetrievedMessages>> GetMessages([NotNull] GetMessages getMessages);

		[OperationContract]
		Task<ServiceResponse> SendMessage([NotNull] SendMessage sendMessage);
	}
}