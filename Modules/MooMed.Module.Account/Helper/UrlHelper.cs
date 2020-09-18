using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
	public class UrlHelper : IUrlHelper
	{
		private const string Localhost = "http://localhost";

		private static Dictionary<string, string> _deploymentUrls = new Dictionary<string, string>()
		{
			{ Environments.Development, $"{Localhost}:1337" },
			{ Environments.Staging, Localhost},
			{ Environments.Production, "http://51.105.229.52" }
		};

		public string GetDeploymentUrl(string environment)
		{
			return _deploymentUrls.TryGetValue(environment, out var deploymentUrl)
				? deploymentUrl
				: Localhost;
		}
	}
}