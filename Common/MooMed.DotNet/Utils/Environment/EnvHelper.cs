using Microsoft.Extensions.Hosting;

namespace MooMed.DotNet.Utils.Environment
{
	public static class EnvHelper
	{
		public static string GetDeployment() => System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production;
	}
}