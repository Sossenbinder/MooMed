using System.Security.Cryptography.X509Certificates;
using EncryptGui.Code.Interface;

namespace EncryptGui.Code
{
	public class CertificateHelper : ICertificateHelper
	{
		public X509Certificate2Collection GetAllCertificatesInStore(StoreName storeName, StoreLocation storeLocation)
		{
			var store = new X509Store(storeName, storeLocation);

			store.Open(OpenFlags.ReadOnly);

			return store.Certificates;
		}
	}
}
