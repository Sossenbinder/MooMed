using System.IO;
using System.Security.Cryptography;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Crypto
{
    public class AESHelper
    {
        [NotNull]
        private readonly byte[] _iv;

        [NotNull]
        private readonly byte[] _key;

        public AESHelper([NotNull] byte[] iv, [NotNull] byte[] key)
        {
            _iv = iv;
            _key = key;
        }

        [NotNull]
        public byte[] Encrypt([NotNull] byte[] toEncrypt)
        {
            using (var aesManaged = PrepareAesManaged())
            {
                using (var msEncrypt = new MemoryStream())
                {
                    using (var cStream = new CryptoStream(msEncrypt, aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV), CryptoStreamMode.Write))
                    {
                        cStream.Write(toEncrypt, 0, toEncrypt.Length);
                        // Adds padding to the encrypted data if it is not adding up to a multiple of 16 bytes
                        cStream.FlushFinalBlock();
                    }

                    return msEncrypt.ToArray();
                }
            }
        }

        [NotNull]
        public byte[] Decrypt([NotNull] byte[] toDecrypt)
        {
            using (var aesManaged = PrepareAesManaged())
            {
                using (var msDecrypt = new MemoryStream())
                {
                    using (var cStream = new CryptoStream(msDecrypt, aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV), CryptoStreamMode.Write))
                    {
                        cStream.Write(toDecrypt, 0, toDecrypt.Length);
                    }

                    return msDecrypt.ToArray();
                }
            }
        }

        [NotNull]
        private AesManaged PrepareAesManaged()
        {
            var aesManaged = new AesManaged()
            {
                IV = _iv,
                Key = _key,
                Padding = PaddingMode.PKCS7
            };

            return aesManaged;
        }
    }
}
