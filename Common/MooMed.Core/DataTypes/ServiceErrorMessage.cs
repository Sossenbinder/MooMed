using MooMed.Core.Translations.Resources;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
	public class ServiceErrorMessage
	{
		private string? _errorMessage;

		[ProtoMember(1)]
		public string Message
		{
			get => _errorMessage ?? Translation.GenericErrorMessage;
			private set => _errorMessage = value;
		}

		private ServiceErrorMessage()
		{
		}

		public ServiceErrorMessage(string? errorMessage = null)
		{
			_errorMessage = errorMessage;
		}
	}
}