using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace EncryptGui.Code.Interface
{
	public interface ICertificateHelper
	{
		[NotNull]
		X509Certificate2Collection GetAllCertificatesInStore(StoreName storeName, StoreLocation storeLocation);
	}
}
