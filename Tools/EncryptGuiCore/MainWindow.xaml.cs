using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using EncryptGui.Code;
using EncryptGui.Code.Interface;
using EncryptGuiCore.Model;
using JetBrains.Annotations;
using MooMed.Core.Code.Helper.Crypto;

namespace EncryptGuiCore
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[UsedImplicitly]
		public CertificateModel CertModel { get; }

		[UsedImplicitly]
		public EncryptDecryptModel CryptoModel { get; }

		private ICertificateHelper _certificateHelper { get; }

		public MainWindow()
		{
			_certificateHelper = new CertificateHelper();
			CertModel = new CertificateModel();
			CryptoModel = new EncryptDecryptModel();

			InitializeComponent();
			InitializeModel();
			InitializeControls();

			DataContext = this;
		}

		private void InitializeModel()
		{
			CertModel.SelectedCertStore = StoreLocation.LocalMachine;
			CertModel.SelectedStoreName = StoreName.My;
			CertModel.Certificates = new List<X509Certificate2>();
		}

		private void InitializeControls()
		{
			SelectedCertStoreComboBox.ItemsSource = Enum.GetValues(typeof(StoreLocation));
			SelectedStoreComboBox.ItemsSource = Enum.GetValues(typeof(StoreName));
		}

		private void CertStoreComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			UpdateCertificatesList();
		}

		private void StoreComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			UpdateCertificatesList();
		}

		private void UpdateCertificatesList()
		{
			CertificateListBox.Items.Clear();

			var certificates = _certificateHelper.GetAllCertificatesInStore(CertModel.SelectedStoreName, CertModel.SelectedCertStore);

			foreach (var cert in certificates)
			{
				CertificateListBox.Items.Add(cert);
			}
		}

		private void CertificateListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var selectedCert = CertificateListBox.SelectedItem as X509Certificate2;

			if (selectedCert != null)
			{
				CertModel.SelectedCertificate = selectedCert;
			}

			EncryptedKeyTextBox.IsEnabled = selectedCert != null;
			EncryptedIvTextBox.IsEnabled = selectedCert != null;
		}

		private void EncryptedKeyTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			var encryptedKey = EncryptedKeyTextBox.Text;

			if (encryptedKey.EndsWith('='))
			{
				var decryptedKey = RSAHelper.DecryptWithCert(CertModel.SelectedCertificate, Convert.FromBase64String(encryptedKey));

				CryptoModel.DecryptedKey = Convert.ToBase64String(decryptedKey);
			}
		}

		private void EncryptedIvTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			var encryptedIv = EncryptedIvTextBox.Text;

			if (encryptedIv.EndsWith('=')) 
			{
				var decryptedIv = RSAHelper.DecryptWithCert(CertModel.SelectedCertificate, Convert.FromBase64String(encryptedIv));

				CryptoModel.DecryptedInitializationVector = Convert.ToBase64String(decryptedIv);
			}
		}

		private void ClearTextData_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			var clearTextData = ClearTextData.Text;

			//if (clearTextData)
		}
	}
}
