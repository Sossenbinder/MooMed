using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.ServiceBase.Interface;
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

		public Task SendMessage(SendMessageModel sendMessageModel)
			=> Invoke(service => service.SendMessage(sendMessageModel));
	}
}
