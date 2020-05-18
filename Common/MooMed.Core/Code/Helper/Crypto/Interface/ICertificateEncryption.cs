﻿using JetBrains.Annotations;

namespace MooMed.Core.Code.Helper.Crypto.Interface
{
	public interface ICertificateEncryption
	{
		[NotNull]
		byte[] Encrypt([NotNull] byte[] toEncrypt);

		[NotNull]
		byte[] Decrypt([NotNull] byte[] toDecrypt);
	}
}