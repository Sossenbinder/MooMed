using System;
using JetBrains.Annotations;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.Helper
{
	public static class DeployedFabricServiceHelper
	{
		public static DeployedFabricService ServiceNameToFabricService([NotNull] string serviceName)
		{
			switch (serviceName)
			{
				case "MooMed.StatelessService.WebType":
					return DeployedFabricService.MooMedWeb;
				case "MooMed.StatefulService.AccountServiceType":
					return DeployedFabricService.MooMedAccountService;
				case "MooMed.StatefulService.AccountValidationServiceType":
					return DeployedFabricService.MooMedAccountValidationService;
				case "MooMed.StatefulService.SearchServiceType":
					return DeployedFabricService.MooMedSearchService;
				case "MooMed.StatefulService.SessionServiceType":
					return DeployedFabricService.MooMedSessionService;
				case "MooMed.StatefulService.ProfilePictureServiceType":
					return DeployedFabricService.MooMedProfilePictureService;
				default:
					throw new ArgumentException();
			}
		}

		[NotNull]
		public static string ServiceNameToFabricService(DeployedFabricService service)
		{
			switch (service)
			{
				case DeployedFabricService.MooMedWeb:
					return "MooMed.StatelessService.WebType";
				case DeployedFabricService.MooMedAccountService:
					return "MooMed.StatefulService.AccountServiceType";
				case DeployedFabricService.MooMedAccountValidationService:
					return "MooMed.StatefulService.AccountValidationServiceType";
				case DeployedFabricService.MooMedSearchService:
					return "MooMed.StatefulService.SearchServiceType";
				case DeployedFabricService.MooMedSessionService:
					return "MooMed.StatefulService.SessionServiceType";
				case DeployedFabricService.MooMedProfilePictureService:
					return "MooMed.StatefulService.ProfilePictureServiceType";
				default:
					throw new ArgumentException();
			}
		}
	}
}
