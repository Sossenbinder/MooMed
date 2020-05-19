using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Crypto.Interface;

namespace MooMed.Core.Code.Helper.Crypto
{
	public class SettingsCryptoProvider : ICryptoProvider
	{
		[NotNull]
		private readonly AESHelper _aesHelper;

		public SettingsCryptoProvider([NotNull] IConfig configuration)
		{
			_aesHelper = InitAesHelper(configuration);
		}

		private AESHelper InitAesHelper([NotNull] IConfig configuration)
		{
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
