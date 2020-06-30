using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace EncryptGui.Model
{
	public class MainWindowModel
	{
		public StoreLocation SelectedCertStore { get; set; }

		public StoreName SelectedStoreName { get; set; }

		public List<X509Certificate2> Certificates { get; set; }

		public X509Certificate2 SelectedCertificate { get; set; }
	}
}
