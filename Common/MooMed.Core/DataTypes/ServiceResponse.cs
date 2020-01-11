using System;
using JetBrains.Annotations;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
    [ProtoContract]
    public abstract class ServiceResponseBase
    {
	    [ProtoMember(1)]
        public ServiceErrorMessage ErrorMessage { get; set; }

        [ProtoMember(2)]
        public bool IsSuccess { get; set; }

        [ProtoMember(3)]
        public bool IsError { get; set; }

        protected ServiceResponseBase()
        {
        }

        protected ServiceResponseBase(bool isSuccess, [CanBeNull] string errorMessage)
        {
            IsSuccess = isSuccess;
            IsError = !isSuccess;
            ErrorMessage = new ServiceErrorMessage(errorMessage);
        }
	}

    [ProtoContract()]
    // Simple base class to transport the result of a backend task to the frontend and provide a way to check whether call was successful
    public class ServiceResponse<TPayload> : ServiceResponseBase
    {
	    [ProtoMember(4)]
	    private TPayload m_payload;

	    [NotNull] 
	    public TPayload PayloadOrNull => m_payload;

        [NotNull]
		public TPayload PayloadOrFail
		{
			get
			{
				if (m_payload == null)
				{
					throw new NullReferenceException();
				}

				return m_payload;
			}
		}

		private ServiceResponse()
		{

		}

		private ServiceResponse(bool isSuccess, [CanBeNull] TPayload payload, [CanBeNull] string errorMessage)
            : base(isSuccess, errorMessage)
        {
	        m_payload = payload;
        }

        [NotNull]
        public static ServiceResponse<TPayload> Success(TPayload payload, [CanBeNull] string errorMessage = null) 
            => new ServiceResponse<TPayload>(true, payload, errorMessage);

        [NotNull]
        public static ServiceResponse<TPayload> Failure([CanBeNull] TPayload payload = default, [CanBeNull] string errorMessage = null) 
            => new ServiceResponse<TPayload>(false, payload, errorMessage);
    }
}
