using System;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.Crypto.Interface;

namespace MooMed.Core.Code.Helper.Crypto
{
    internal class SettingsCrypto : ISettingsCrypto
    {
	    [NotNull]
        private readonly ICryptoProvider _cryptoProvider;

        public SettingsCrypto([NotNull] ICryptoProvider cryptoProvider)
        {
	        _cryptoProvider = cryptoProvider;
        }
        
        public string EncryptSetting(string setting, string parameterToEncrypt)
        {
	        var cryptoParam = GetParameterToPerformCrypto(setting, parameterToEncrypt);

            var encrypted = _cryptoProvider.Encrypt(Convert.FromBase64String(cryptoParam));

            return ReplaceCryptedParamInSetting(setting, parameterToEncrypt, encrypted);
        }

        public string DecryptSetting(string setting, string parameterToDecrypt)
        {
	        if (parameterToDecrypt != null)
            {
                var cryptoParam = GetParameterToPerformCrypto(setting, parameterToDecrypt);

                var decrypted = _cryptoProvider.Decrypt(Convert.FromBase64String(cryptoParam));

                return ReplaceCryptedParamInSetting(setting, parameterToDecrypt, decrypted);
            }
            else
            {
                var decrypted = _cryptoProvider.Decrypt(Convert.FromBase64String(setting));

                return Convert.ToBase64String(decrypted);
            }
        }

        [NotNull]
        private static string GetParameterToPerformCrypto([NotNull] string value, [NotNull] string parameterToDecrypt)
        {
            if (!value.Contains(parameterToDecrypt))
            {
                throw new ArgumentException($"Parameter {parameterToDecrypt} not found in {value}");
            }

            var startingIndexOfParam = GetStartingIndexOfParameter(value, parameterToDecrypt);

            return value.Substring(startingIndexOfParam, value.IndexOf(";", startingIndexOfParam, StringComparison.Ordinal) - startingIndexOfParam);
        }

        private static int GetStartingIndexOfParameter([NotNull] string value, [NotNull] string parameterToDecrypt)
        {
            return value.IndexOf("=", value.IndexOf(parameterToDecrypt, StringComparison.Ordinal), StringComparison.Ordinal) + 1;
        }

        [NotNull]
        private string ReplaceCryptedParamInSetting([NotNull] string setting, [NotNull] string parameterToCrypt, [NotNull] byte[] crypted)
        {
            var cryptedAsString = Convert.ToBase64String(crypted);

            return setting.Replace(GetParameterToPerformCrypto(setting, parameterToCrypt), cryptedAsString);
        }
    }
}
