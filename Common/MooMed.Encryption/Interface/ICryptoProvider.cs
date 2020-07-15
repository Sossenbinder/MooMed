namespace MooMed.Encryption.Interface
{
	public interface ICryptoProvider
	{
		byte[] Encrypt(byte[] toEncrypt);

		byte[] Decrypt(byte[] toDecrypt);
	}
}
