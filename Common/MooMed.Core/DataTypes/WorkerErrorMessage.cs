using System.Runtime.Serialization;
using JetBrains.Annotations;
using MooMed.Core.Translations.Resources;

namespace MooMed.Core.DataTypes
{
	[DataContract]
    public class WorkerErrorMessage
    {
		[DataMember]
        [CanBeNull]
        private readonly string m_errorMessage;

        public string Message => m_errorMessage ?? Translation.GenericErrorMessage;

        public WorkerErrorMessage([CanBeNull] string errorMessage = null)
        {
            m_errorMessage = errorMessage;
        }
    }
}
