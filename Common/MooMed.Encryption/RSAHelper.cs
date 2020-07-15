using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace MooMed.Encryption
{
    public static class RSAHelper
    {
        public static byte[] EncryptWithCert(X509Certificate2 cert, byte[] payload)
        {
	        using var rsaPublicKey = cert.GetRSAPublicKey();

	        return rsaPublicKey.Encrypt(payload, RSAEncryptionPadding.OaepSHA256);
        }

        public static byte[] DecryptWithCert(X509Certificate2 cert, byte[] payload)
        {
	        using var rsaPrivateKey = cert.GetRSAPrivateKey();

	        return rsaPrivateKey.Decrypt(payload, RSAEncryptionPadding.OaepSHA256);
        }
    }
}
