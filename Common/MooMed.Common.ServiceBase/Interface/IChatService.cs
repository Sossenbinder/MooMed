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
		Task<ServiceResponse<RetrievedMessages>> GetMessages([NotNull] GetMessages getMessages);

		[OperationContract]
		Task<ServiceResponse> SendMessage([NotNull] SendMessage sendMessage);
	}
}
