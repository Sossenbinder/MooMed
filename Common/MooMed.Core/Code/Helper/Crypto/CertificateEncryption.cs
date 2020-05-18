using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Crypto.Interface;

namespace MooMed.Core.Code.Helper.Crypto
{
	public class CertificateEncryption : ICertificateEncryption
	{
		[NotNull]
		private readonly AESHelper _aesHelper;

		public CertificateEncryption([NotNull] IConfig configuration)
		{
			_aesHelper = InitCryptoData(configuration);
		}

		private AESHelper InitCryptoData([NotNull] IConfig configuration)
		{
			try
			{
				var certPath = "/usr/local/share/ca-certificates/moomed.pfx";

				var cert = new X509Certificate2(File.ReadAllBytes(certPath), configuration["MooMed.CertKey"]);

				var encryptedIv = configuration["MooMed_IV"];
				var encryptedKey = configuration["MooMed_Key"];

				var decryptedIv = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedIv));
				var decryptedKey = RSAHelper.DecryptWithCert(cert, Convert.FromBase64String(encryptedKey));

				return new AESHelper(decryptedIv, decryptedKey);
			}
			catch (CryptographicException exc)
			{
				return null;
			}
		}

		public byte[] Encrypt(byte[] toEncrypt)
		{
			return _aesHelper.Encrypt(toEncrypt);
		}

		public byte[] Decrypt(byte[] toDecrypt)
		{
			return _aesHelper.Decrypt(toDecrypt);
		}
	}
}
