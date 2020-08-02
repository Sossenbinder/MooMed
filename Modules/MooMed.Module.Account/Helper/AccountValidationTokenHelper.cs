using System;
using System.Text;
using JetBrains.Annotations;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
	public class AccountValidationTokenHelper : IAccountValidationTokenHelper
	{
		private readonly Encoding _encoding;

		public AccountValidationTokenHelper(Encoding defaultEncoding)
		{
			_encoding = defaultEncoding;
		}

		[NotNull]
		public string Serialize(string rawToken)
		{
			return Convert.ToBase64String(_encoding.GetBytes(rawToken));
		}

		[NotNull]
		public string Deserialize([NotNull] string tokenRaw)
		{
			return _encoding.GetString(Convert.FromBase64String(tokenRaw));
		}
	}
}