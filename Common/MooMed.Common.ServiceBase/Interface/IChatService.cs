using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IChatService : IGrpcService
	{
		[OperationContract]
		Task<ServiceResponse<RetrievedMessagesModel>> GetMessages([NotNull] GetMessagesModel getMessagesModel);

		[OperationContract]
		Task<ServiceResponse> SendMessage([NotNull] SendMessageModel sendMessageModel);
	}
}
