using System;
using JetBrains.Annotations;
using MooMed.ServiceRemoting.DataType;

namespace MooMed.ServiceRemoting.Helper
{
	public class DeployedFabricApplicationHelper
	{
		public static DeployedFabricApplication FabricApplicationToApplicationName([NotNull] string applicationName)
		{
			switch (applicationName)
			{
				case "MooMed.Fabric":
					return DeployedFabricApplication.MooMed;
				default:
					throw new ArgumentException();
			}
		}

		[NotNull]
		public static string FabricApplicationToApplicationName(DeployedFabricApplication application)
		{
			switch (application)
			{
				case DeployedFabricApplication.MooMed:
					return "MooMed.FabricType";
				default:
					throw new ArgumentException();
			}
		}
	}
}
