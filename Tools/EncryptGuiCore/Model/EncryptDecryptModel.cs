using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace EncryptGuiCore.Model
{
	public class EncryptDecryptModel : INotifyPropertyChanged
	{
		public string EncryptedInitializationVector { get; set; }

		public string EncryptedKey { get; set; }

		private string _decryptedInitializationVector;

		public string DecryptedInitializationVector
		{
			get => _decryptedInitializationVector;
			set
			{
				_decryptedInitializationVector = value;
				OnPropertyChanged();
			}
		}

		private string _decryptedKey;

		public string DecryptedKey
		{
			get => _decryptedKey;
			set
			{
				_decryptedKey = value;
				OnPropertyChanged();
			}
		}

		public string EncryptedData { get; set; }

		public string ClearTextData { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
