using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Crypto
{
    public static class RSAHelper
    {
        public static byte[] EncryptWithCert([NotNull] X509Certificate2 cert, byte[] payload)
        {
            using (var rsaPublicKey = cert.GetRSAPublicKey())
            {
                return rsaPublicKey.Encrypt(payload, RSAEncryptionPadding.OaepSHA256);
            }
        }

        public static byte[] DecryptWithCert([NotNull] X509Certificate2 cert, byte[] payload)
        {
            using (var rsaPrivateKey = cert.GetRSAPrivateKey())
            {
                return rsaPrivateKey.Decrypt(payload, RSAEncryptionPadding.OaepSHA256);
            }
        }
    }
}
