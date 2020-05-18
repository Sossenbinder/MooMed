using System.ServiceModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Grpc.Definitions.Interface;

namespace MooMed.Common.ServiceBase.Interface
{
	[ServiceContract]
	public interface IChatService : IGrpcService
	{
		[OperationContract]
		Task SendMessage([NotNull] SendMessageModel sendMessageModel);
	}
}
