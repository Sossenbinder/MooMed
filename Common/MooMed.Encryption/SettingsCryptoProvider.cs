using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using MooMed.Encryption.Interface;

namespace MooMed.Encryption
{
	public class SettingsCryptoProvider : ICryptoProvider
	{
		[NotNull]
		private readonly AESHelper _aesHelper;

		public SettingsCryptoProvider([NotNull] IConfiguration configuration)
		{
			_aesHelper = InitAesHelper(configuration);
		}

		private AESHelper InitAesHelper([NotNull] IConfiguration configuration)
		{
			// Coming from Keyvault!
			var plainIv = configuration["MooMedIV"];
			var plainKey = configuration["MooMedKey"];

			var decryptedIv = Convert.FromBase64String(plainIv);
			var decryptedKey = Convert.FromBase64String(plainKey);

			return new AESHelper(decryptedIv, decryptedKey);
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
