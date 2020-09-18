using System.Threading.Tasks;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using MooMed.ServiceBase.Services.Interface;

namespace MooMed.ChatService.Remoting
{
	public class ChatServiceProxy : AbstractDeploymentProxy<IChatService>, IChatService
	{
		public ChatServiceProxy(IGrpcClientProvider clientProvider)
			: base(clientProvider,
				DeploymentService.ChatService)
		{ }

		public Task<ServiceResponse<RetrievedMessages>> GetMessages(GetMessages getMessages)
			=> InvokeWithResult(service => service.GetMessages(getMessages));

		public Task<ServiceResponse> SendMessage(SendMessage sendMessage)
			=> InvokeWithResult(service => service.SendMessage(sendMessage));
	}
}