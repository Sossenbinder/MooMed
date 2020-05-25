using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;

namespace MooMed.ChatService.Remoting
{
	public class ChatServiceProxy : AbstractDeploymentProxy<IChatService>, IChatService
	{
		public ChatServiceProxy([NotNull] IGrpcClientProvider clientProvider) 
			: base(
				clientProvider, 
				MooMedService.ChatService)
		{ }

		public Task<ServiceResponse<RetrievedMessagesModel>> GetMessages(GetMessagesModel getMessagesModel)
			=> InvokeWithResult(service => service.GetMessages(getMessagesModel));

		public Task<ServiceResponse> SendMessage(SendMessageModel sendMessageModel)
			=> InvokeWithResult(service => service.SendMessage(sendMessageModel));
	}
}
