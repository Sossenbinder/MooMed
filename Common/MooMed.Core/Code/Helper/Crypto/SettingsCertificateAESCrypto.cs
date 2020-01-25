using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Crypto.Interface;

namespace MooMed.Core.Code.Helper.Crypto
{
    public class SettingsCertificateCrypto : ISettingsCrypto
    {
        [CanBeNull]
        private AESHelper m_aesHelper;

        [NotNull]
        private readonly IConfigSettingsAccessor m_configSettingsAccessor;

        public SettingsCertificateCrypto(
            [NotNull] IConfigSettingsAccessor configSettingsAccessor)
        {
            m_configSettingsAccessor = configSettingsAccessor;
            InitCryptoData();
        }

        private void InitCryptoData()
        {
            try
            {
	            var certPath = "/usr/local/share/ca-certificates/moomed.pfx";
	            var cert = new X509Certificate2(File.ReadAllBytes(certPath), "xfvesXTs1fFhm5yKsb9H");


                var encryptedIv = m_configSettingsAccessor.GetValueFromConfigSource("MooMed_IV");
                var encryptedKey = m_configSettingsAccessor.GetValueFromConfigSource("MooMed_Key");

                var decryptedIv = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedIv));
                var decryptedKey = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedKey));

                m_aesHelper = new AESHelper(decryptedIv, decryptedKey);
            }
            catch (CryptographicException exc)
            {

            }
        }

        public string EncryptSetting(string setting, string parameterToEncrypt)
        {
            if (m_aesHelper == null)
            {
                return null;
            }

            var cryptoParam = GetParameterToPerformCrypto(setting, parameterToEncrypt);

            var encrypted = m_aesHelper.Encrypt(Convert.FromBase64String(cryptoParam));

            return ReplaceCryptedParamInSetting(setting, parameterToEncrypt, encrypted);
        }

        public string DecryptSetting(string setting, string parameterToDecrypt)
        {
            if (m_aesHelper == null)
            {
                return null;
            }

            if (parameterToDecrypt != null)
            {
                var cryptoParam = GetParameterToPerformCrypto(setting, parameterToDecrypt);

                var decrypted = m_aesHelper.Decrypt(Convert.FromBase64String(cryptoParam));

                return ReplaceCryptedParamInSetting(setting, parameterToDecrypt, decrypted);
            }
            else
            {
                var decrypted = m_aesHelper.Decrypt(Convert.FromBase64String(setting));

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
