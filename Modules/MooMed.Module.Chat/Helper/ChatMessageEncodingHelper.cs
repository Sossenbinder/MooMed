using System;
using System.Text;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.Crypto.Interface;

namespace MooMed.Module.Chat.Helper
{
	public class ChatMessageEncodingHelper
	{
		[NotNull]
		private readonly ICryptoProvider _cryptoProvider;

		public ChatMessageEncodingHelper([NotNull] ICryptoProvider cryptoProvider)
		{
			_cryptoProvider = cryptoProvider;
		}

		public string Encode([NotNull] string message)
		{
			var originalMessageAsBytes = Encoding.Unicode.GetBytes(message);
			var encodedMessage = Convert.ToBase64String(originalMessageAsBytes);
			var encodedBytes = Convert.FromBase64String(encodedMessage);

			var encryptedMessageAsBytes = _cryptoProvider.Encrypt(encodedBytes);
			var encryptedMessageText = Convert.ToBase64String(encryptedMessageAsBytes);

			return encryptedMessageText;
		}

		public string Decode([NotNull] string message)
		{
			var encryptedMessageAsBytes = Convert.FromBase64String(message);
			var decryptedMessageAsBytes = _cryptoProvider.Decrypt(encryptedMessageAsBytes);

			var encodedMsgAsString = Convert.ToBase64String(decryptedMessageAsBytes);
			var encodedMsgAsBytes = Convert.FromBase64String(encodedMsgAsString);
			var originalMessage = Encoding.Unicode.GetString(encodedMsgAsBytes);

			return originalMessage;
		}
	}
}
