using System;
using System.Security.Cryptography;
using JetBrains.Annotations;
using MooMed.Configuration.Interface;
using MooMed.Encryption;
using MooMed.Encryption.Interface;

namespace MooMed.TestBase.Config
{
	public class UnitTestCryptoProvider : ICryptoProvider
	{
		[NotNull]
		private readonly AESHelper _aesHelper;

		public UnitTestCryptoProvider([NotNull] IConfig configuration)
		{
			_aesHelper = InitCryptoData(configuration);
		}

		private AESHelper InitCryptoData([NotNull] IConfig configuration)
		{
			try
			{
				var iv = configuration["MooMed_IV"];
				var key = configuration["MooMed_Key"];

				return new AESHelper(Convert.FromBase64String(iv), Convert.FromBase64String(key));
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
