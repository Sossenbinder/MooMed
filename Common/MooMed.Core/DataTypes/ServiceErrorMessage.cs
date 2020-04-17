using JetBrains.Annotations;
using MooMed.Core.Translations.Resources;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
    public class ServiceErrorMessage
    {
        [CanBeNull]
        private string _errorMessage;

        [ProtoMember(1)]
        public string Message
        {
	        get => _errorMessage ?? Translation.GenericErrorMessage;
	        private set => _errorMessage = value;
        }

        private ServiceErrorMessage()
        {
        }

        public ServiceErrorMessage([CanBeNull] string errorMessage = null)
        {
            _errorMessage = errorMessage;
        }
    }
}
