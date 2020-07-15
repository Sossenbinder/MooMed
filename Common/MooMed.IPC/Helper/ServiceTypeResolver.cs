using System;
using System.Collections.Generic;
using MooMed.Common.Definitions.IPC;

namespace MooMed.IPC.Helper
{
	public static class ServiceTypeResolver
	{
		private static Dictionary<MooMedService, ServiceType> _serviceTypeDict = new Dictionary<MooMedService, ServiceType>()
		{
			{ MooMedService.AccountService, ServiceType.StatefulSet },
			{ MooMedService.AccountValidationService, ServiceType.StatefulSet },
			{ MooMedService.SearchService, ServiceType.Deployment },
			{ MooMedService.SessionService, ServiceType.StatefulSet },
			{ MooMedService.ChatService, ServiceType.Deployment },
			{ MooMedService.ProfilePictureService, ServiceType.Deployment },
			{ MooMedService.FinanceService, ServiceType.Deployment },
		};
		
		public static ServiceType GetServiceTypeForService(MooMedService moomedService)
		{
			return _serviceTypeDict[moomedService];
		}
		
		public static DeploymentService TranslateMooMedServiceToDeploymentService(MooMedService moomedService) => ParseService<DeploymentService>(moomedService);

		public static StatefulSetService TranslateMooMedServiceToStatefulSetService(MooMedService moomedService) => ParseService<StatefulSetService>(moomedService);

		private static TOut ParseService<TOut>(MooMedService mooMedService) 
			where TOut : struct
		{
			return Enum.Parse<TOut>(mooMedService.ToString());
		}
	}
}
