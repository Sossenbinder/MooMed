using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MooMed.Configuration.Interface;

namespace MooMed.Configuration
{
	public class Config : IConfig
	{
		private readonly IConfiguration _configuration;

		public Config(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IConfigurationSection GetSection(string key) => _configuration.GetSection(key);

		public IEnumerable<IConfigurationSection> GetChildren() => _configuration.GetChildren();

		public IChangeToken GetReloadToken() => _configuration.GetReloadToken();

		public string this[string key]
		{
			get => _configuration[key];
			set => _configuration[key] = value;
		}
	}
}
