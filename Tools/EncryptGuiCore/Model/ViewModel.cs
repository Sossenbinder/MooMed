using System.ComponentModel;
using System.Runtime.CompilerServices;
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

		private string _clearTextData;

		public string ClearTextData
		{
			get => _clearTextData;
			set
			{
				_clearTextData = value;
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