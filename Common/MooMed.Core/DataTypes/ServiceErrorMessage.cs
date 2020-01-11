using JetBrains.Annotations;
using MooMed.Core.Translations.Resources;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
    public class ServiceErrorMessage
    {
        [CanBeNull]
        private string m_errorMessage;

        [ProtoMember(1)]
        public string Message
        {
	        get => m_errorMessage ?? Translation.GenericErrorMessage;
	        private set => m_errorMessage = value;
        }

        private ServiceErrorMessage()
        {
        }

        public ServiceErrorMessage([CanBeNull] string errorMessage = null)
        {
            m_errorMessage = errorMessage;
        }
    }
}
