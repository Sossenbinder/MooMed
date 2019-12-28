using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using EncryptGui.Code;
using EncryptGui.Code.Interface;
using EncryptGui.Model;
using JetBrains.Annotations;

namespace EncryptGui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[UsedImplicitly]
		public MainWindowModel Model { get; }

		private ICertificateHelper m_certificateHelper { get; }

		public MainWindow()
		{
			m_certificateHelper = new CertificateHelper();
			Model = new MainWindowModel();

			InitializeComponent();
			InitializeModel();
			InitializeControls();

			DataContext = this;
		}

		private void InitializeModel()
		{
			Model.SelectedCertStore = StoreLocation.LocalMachine;
			Model.SelectedStoreName = StoreName.My;
			Model.Certificates = new List<X509Certificate2>();
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

			var certificates = m_certificateHelper.GetAllCertificatesInStore(Model.SelectedStoreName, Model.SelectedCertStore);

			foreach (var cert in certificates)
			{
				CertificateListBox.Items.Add(cert);
			}
		}

		private void CertificateListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}
	}
}
