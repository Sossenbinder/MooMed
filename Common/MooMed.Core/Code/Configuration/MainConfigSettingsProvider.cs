using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Crypto.Interface;

namespace MooMed.Core.Code.Configuration
{
    public class MainConfigSettingsProvider : IConfigSettingsProvider
    {
	    [NotNull]
	    private readonly IConfig _config;

	    [NotNull]
        private readonly ISettingsCrypto _settingsCrypto;

        public MainConfigSettingsProvider(
			[NotNull] IConfig config,
            [NotNull] ISettingsCrypto settingsCrypto)
        {
	        _config = config;
	        _settingsCrypto = settingsCrypto;
        }

        public T ReadValueOrDefault<T>(string key)
        {
	        var value = _config[key];

	        if (value == null)
	        {
		        return default;
	        }

	        return (T)Convert.ChangeType(value, typeof(T));
        }

        public T ReadValueOrFail<T>(string key)
        {
            var value = ReadValueOrDefault<T>(key);

            if (EqualityComparer<T>.Default.Equals(value, default))
            {
                throw new ArgumentException("Key not found");
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T ReadDecryptedValueOrDefault<T>(string key, string parameterToDecrypt = null)
        {
            var value = _settingsCrypto.DecryptSetting(ReadValueOrDefault<string>(key) ?? throw new ArgumentException("Key not found"), parameterToDecrypt);

            if (value == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T ReadDecryptedValueOrFail<T>(string key, string parameterToDecrypt = null)
        {
            var value = ReadDecryptedValueOrDefault<T>(key, parameterToDecrypt);

            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                throw new ArgumentException("Key not found");
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
