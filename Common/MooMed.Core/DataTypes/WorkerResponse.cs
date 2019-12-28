using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace MooMed.Core.DataTypes
{
    [DataContract]
    public abstract class WorkerResponseBase
    {
        [DataMember]
        public WorkerErrorMessage ErrorMessage { get; private set; }

        [DataMember]
        public bool IsSuccess { get; private set; }

		[DataMember]
		public bool IsError { get; private set; }

        protected WorkerResponseBase(bool isSuccess, [CanBeNull] string errorMessage)
        {
            IsSuccess = isSuccess;
            IsError = !isSuccess;
            ErrorMessage = new WorkerErrorMessage(errorMessage);
        }
	}

    [DataContract]
    // Simple base class to transport the result of a backend task to the frontend and provide a way to check whether call was successful
    public class WorkerResponse<TPayload> : WorkerResponseBase
	{
		[DataMember]
		private TPayload m_payLoad { get; set; }

        public TPayload PayloadOrNull => m_payLoad;

		[NotNull]
		public TPayload PayloadOrFail
		{
			get
			{
				if (m_payLoad == null)
				{
					throw new NullReferenceException();
				}

				return m_payLoad;
			}
		}


		private WorkerResponse(bool isSuccess, [CanBeNull] TPayload payload, [CanBeNull] string errorMessage)
            : base(isSuccess, errorMessage)
        {
	        m_payLoad = payload;
        }

        [NotNull]
        public static WorkerResponse<TPayload> Success(TPayload payload, [CanBeNull] string errorMessage = null) 
            => new WorkerResponse<TPayload>(true, payload, errorMessage);

        [NotNull]
        public static WorkerResponse<TPayload> Failure([CanBeNull] TPayload payload = default, [CanBeNull] string errorMessage = null) 
            => new WorkerResponse<TPayload>(false, payload, errorMessage);
    }
}
