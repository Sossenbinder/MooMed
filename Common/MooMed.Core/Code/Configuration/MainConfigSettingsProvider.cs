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
        private readonly IEnumerable<IConfigSettingsAccessor> m_configSettingsAccessors;

        [NotNull]
        private readonly ISettingsCrypto m_settingsCrypto;

        public MainConfigSettingsProvider(
			[NotNull] IEnumerable<IConfigSettingsAccessor> configSettingsAccessors,
            [NotNull] ISettingsCrypto settingsCrypto)
        {
            m_configSettingsAccessors = configSettingsAccessors;
            m_settingsCrypto = settingsCrypto;
        }

        public T ReadValueOrDefault<T>(string key)
        {
	        foreach (var configSettingsAccessor in m_configSettingsAccessors)
	        {
		        var value = configSettingsAccessor.GetValueFromConfigSource(key);

		        if (value == null)
		        {
			        return default;
		        }

		        return (T)Convert.ChangeType(value, typeof(T));
	        }

	        return default;
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
            var value = m_settingsCrypto.DecryptSetting(ReadValueOrDefault<string>(key) ?? throw new InvalidOperationException(), parameterToDecrypt);

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
