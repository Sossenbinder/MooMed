﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.IPC;
using MooMed.Common.Definitions.Models.Session.Interface;
using MooMed.Common.ServiceBase.Interface;
using MooMed.Core.DataTypes;
using MooMed.IPC.Grpc.Interface;
using MooMed.IPC.ProxyInvocation;
using ProtoBuf.Grpc;

namespace MooMed.Stateful.ProfilePictureService.Remoting
{
	public class ProfilePictureServiceProxy : AbstractDeploymentProxy<IProfilePictureService>, IProfilePictureService
	{
		public ProfilePictureServiceProxy([NotNull] IGrpcClientProvider grpcClientProvider)
			: base(
				grpcClientProvider,
				MooMedService.ProfilePictureService)
		{
		}

		public Task<ServiceResponse<bool>> ProcessUploadedProfilePicture(IAsyncEnumerable<byte[]> pictureStream, CallContext callContext)
			=> InvokeWithResult(service => service.ProcessUploadedProfilePicture(pictureStream, callContext));

		public Task<ServiceResponse<string>> GetProfilePictureForAccountById(Primitive<int> accountId)
			=> InvokeWithResult(service => service.GetProfilePictureForAccountById(accountId));

		public Task<ServiceResponse<string>> GetProfilePictureForAccount(ISessionContext sessionContext)
			=> InvokeWithResult(service => service.GetProfilePictureForAccount(sessionContext));
	}
}
