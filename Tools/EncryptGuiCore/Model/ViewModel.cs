using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace EncryptGuiCore.Model
{
	public class ViewModel : INotifyPropertyChanged
	{
		public string KeyVaultSecret { get; set; }

		private string _encryptedData;

		public string EncryptedData 
		{ 
			get => _encryptedData;
			set
			{
				_encryptedData = value;
				OnPropertyChanged();
			}
		}

		public string ClearTextData { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
