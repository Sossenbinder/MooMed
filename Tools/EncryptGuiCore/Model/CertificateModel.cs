using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace EncryptGuiCore.Model
{
	public class CertificateModel : INotifyPropertyChanged
	{
		public StoreLocation SelectedCertStore { get; set; }

		public StoreName SelectedStoreName { get; set; }

		public List<X509Certificate2> Certificates { get; set; }

		private X509Certificate2 _selectedCertificate;

		public X509Certificate2 SelectedCertificate
		{
			get => _selectedCertificate;
			set
			{
				_selectedCertificate = value;
				OnPropertyChanged();
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
