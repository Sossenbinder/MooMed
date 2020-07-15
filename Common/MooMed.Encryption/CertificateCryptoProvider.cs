using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using MooMed.Encryption.Interface;

namespace MooMed.Encryption
{
	public class CertificateCryptoProvider : ICryptoProvider
	{
		[NotNull]
		private readonly AESHelper _aesHelper;

		public CertificateCryptoProvider([NotNull] IConfiguration configuration)
		{
			_aesHelper = InitCryptoData(configuration);
		}

		private AESHelper InitCryptoData([NotNull] IConfiguration configuration)
		{
			try
			{
				var test = configuration["MooMedIV"];
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
