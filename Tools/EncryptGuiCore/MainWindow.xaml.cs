﻿using System;
using System.IO;
using System.Windows;
using EncryptGuiCore.Model;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using MooMed.Configuration;
using MooMed.Encryption;

namespace EncryptGuiCore
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[UsedImplicitly]
		public ViewModel Model { get; }

		private IConfigurationSection _keyVaultConfigSection;

		private SettingsCryptoProvider _settingsCryptoProvider;

		public MainWindow()
		{
			Model = new ViewModel();

			InitializeComponent();
			InitializeConfiguration();

			DataContext = this;
		}

		private void InitializeConfiguration()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true);

			_keyVaultConfigSection = builder.Build().GetSection("KeyVault");
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var token = KeyVaultSecretTextBox.Text;

			var config = new ConfigurationBuilder()
				.AddAzureKeyVault(_keyVaultConfigSection["Uri"], "82259ded-f445-4db4-80d4-1c07f13c50b7", token);

			_settingsCryptoProvider = new SettingsCryptoProvider(new Config(config.Build()));

			EncryptedData.IsEnabled = true;
			ClearTextData.IsEnabled = true;
		}

		private void ClearTextData_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			var clearTextData = ClearTextData.Text;

			var encrypted = Convert.ToBase64String(_settingsCryptoProvider.Encrypt(Convert.FromBase64String(clearTextData)));

			Model.EncryptedData = encrypted;
		}
	}
}