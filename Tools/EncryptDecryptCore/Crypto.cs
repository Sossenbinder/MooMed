using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.Crypto;

namespace EncryptDecryptCore
{
    public class Crypto
    {
        private readonly AESHelper m_aesHelper;

        public Crypto()
        {
            var encryptedIv = "mN/xDqijx3EVb69j98OdNueyrkuqjIHuVWCikALjLi5umlg4iS713N6ZKoXcsL+xkmIlEr3DXoDWGXO/LuvS0zs11f9yNbOpN7et0OU3xIH/T3suy4vPN3YXO86UkFCISEU/ntQ9CO+xkPgTGlY/iTGnEDcApVwOpFGFe33PpXWyPVW3VBU4yg3fH4ZRlAe93EPa6tLrXs73pBmpXsFCxHy03+rWTqIX/Uz6MGhoe+UIgun0LoHYaanG8LRswoZgCWPNGtGxwwrG0bcSREJThuDRWYWc6Rb7hn/MbFnfekFHdUWrIetYHTDt7iRXt/pj0bluclEGUNoGvLxGy7Sspg==";
            var encryptedKey = "izyTzc49vAfJ4x5wZSq+EZBhGBtLlAQiCcFnRQlMhV6+3fJT1z35m4ITJ3/XE2YX+oNu9Z+UtVCGXN64+Izm+EAzZZiPk+cZ4OQ0wXQ2RoTnHsQODmpdl3oGltYOA6AzGvlVwQ6N4c6zZ4ikHKWqcn0gu72Adg259YpiJdcTwAaO2tO1C7Z6ykwYk2p2PPvG8GNmnevXu2YYiiNhswP35R2/CrK2YcWpxNhkAydsvLG8YTFDCQjsO+D527nCVu33TDKpbE1xymeZadz4jmPqAnpowUyjzLotjwPGncfmv1bHKEpjCckb7RjEvc6Y4gr0K51WW6mRV7Ms0ks4abcJFQ==";

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);

            store.Open(OpenFlags.OpenExistingOnly);

            foreach (var cert in store.Certificates)
            {
                if (cert.IssuerName.Name == null || !cert.IssuerName.Name.Contains("MooMed"))
                {
                    continue;
                }

                var decryptedIv = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedIv));
                var decryptedKey = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedKey));

                m_aesHelper = new AESHelper(decryptedIv, decryptedKey);
            }
        }

        [NotNull]
        public string Encrypt([NotNull] string toEncrypt)
		{
			toEncrypt = toEncrypt.Replace('-', '+').Replace('_', '/').PadRight(4 * ((toEncrypt.Length + 3) / 4), '=');
			var encrypted = m_aesHelper.Encrypt(Convert.FromBase64String(toEncrypt));

            return Convert.ToBase64String(encrypted);
        }

        [NotNull]
        public string Decrypt([NotNull] string toDecrypt)
        {
            var decrypted = m_aesHelper.Decrypt(Convert.FromBase64String(toDecrypt));

            return Convert.ToBase64String(decrypted);
        }
    }
}
