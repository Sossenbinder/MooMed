using System;
using System.Collections.Generic;
using MooMed.Common.Definitions.Configuration;
using MooMed.Configuration.Interface;
using MooMed.Encryption.Interface;

namespace MooMed.Configuration
{
	public class ConfigProvider : IConfigProvider
	{
		private readonly IConfig _config;

		private readonly ISettingsCrypto _settingsCrypto;

		public ConfigProvider(
			IConfig config,
			ISettingsCrypto settingsCrypto)
		{
			_config = config;
			_settingsCrypto = settingsCrypto;
		}

		public T ReadValue<T>(string key, T optionalValueIfNotFound = default)
		{
			var value = _config[key];

			if (value == null)
			{
				return optionalValueIfNotFound;
			}

			return (T)Convert.ChangeType(value, typeof(T));
		}

		public T ReadValueOrFail<T>(string key)
		{
			var value = ReadValue<T>(key);

			if (EqualityComparer<T>.Default.Equals(value, default))
			{
				throw new ArgumentException("Key not found");
			}

			return (T)Convert.ChangeType(value, typeof(T));
		}

		public T ReadDecryptedValue<T>(string key, string? parameterToDecrypt = null)
		{
			var value = _settingsCrypto.DecryptSetting(ReadValue<string>(key) ?? throw new ArgumentException("Key not found"), parameterToDecrypt);

			if (value == null)
			{
				return default(T);
			}

			return (T)Convert.ChangeType(value, typeof(T));
		}

		public T ReadDecryptedValueOrFail<T>(string key, string? parameterToDecrypt = null)
		{
			var value = ReadDecryptedValue<T>(key, parameterToDecrypt);

			if (EqualityComparer<T>.Default.Equals(value, default(T)))
			{
				throw new ArgumentException("Key not found");
			}

			return (T)Convert.ChangeType(value, typeof(T));
		}
	}
}