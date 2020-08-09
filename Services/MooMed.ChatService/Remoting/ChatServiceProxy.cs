using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Core.DataTypes;
using MooMed.Grpc.Services.Interface;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;

namespace MooMed.ChatService.Remoting
{
	public class ChatServiceProxy : AbstractDeploymentProxy<IChatService>, IChatService
	{
		public ChatServiceProxy([NotNull] IGrpcClientProvider clientProvider)
			: base(
				clientProvider,
				DeploymentService.ChatService)
		{ }

		public Task<ServiceResponse<RetrievedMessages>> GetMessages(GetMessages getMessages)
			=> InvokeWithResult(service => service.GetMessages(getMessages));

		public Task<ServiceResponse> SendMessage(SendMessage sendMessage)
			=> InvokeWithResult(service => service.SendMessage(sendMessage));
	}
}