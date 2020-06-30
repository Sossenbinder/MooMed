using JetBrains.Annotations;
using ProtoBuf;
using System;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
	[ProtoInclude(3253, typeof(ServiceResponse))]
	public abstract class ServiceResponseBase
	{
		[ProtoMember(1)]
		public ServiceErrorMessage ErrorMessage { get; set; }

		[ProtoMember(2)]
		public bool IsSuccess { get; set; }

		[ProtoMember(3)]
		public bool IsFailure { get; set; }

		protected ServiceResponseBase()
		{
		}

		protected ServiceResponseBase(bool isSuccess, [CanBeNull] string errorMessage)
		{
			IsSuccess = isSuccess;
			IsFailure = !isSuccess;
			ErrorMessage = new ServiceErrorMessage(errorMessage);
		}
	}

	[ProtoContract]
	public class ServiceResponse : ServiceResponseBase
	{
		// Implicitly used by protobuf-net, so the lib can use this instead of having to fallback onto the non-parameterless constructor
		[UsedImplicitly]
		private ServiceResponse()
		{

		}

		public ServiceResponse(bool isSuccess, [CanBeNull] string errorMessage = null)
			: base(isSuccess, errorMessage)
		{
		}

		[NotNull]
		public static ServiceResponse Success([CanBeNull] string errorMessage = null)
			=> new ServiceResponse(true, errorMessage);

		[NotNull]
		public static ServiceResponse Failure([CanBeNull] string errorMessage = null)
			=> new ServiceResponse(false, errorMessage);
	}

	[ProtoContract]
	// Simple base class to transport the result of a backend task to the frontend and provide a way to check whether call was successful
	public class ServiceResponse<TPayload> : ServiceResponseBase
	{
		[ProtoMember(4)]
		private TPayload _payload;

		[NotNull]
		public TPayload PayloadOrNull => _payload;

		[NotNull]
		public TPayload PayloadOrFail
		{
			get
			{
				if (_payload == null)
				{
					throw new NullReferenceException();
				}

				return _payload;
			}
		}

		private ServiceResponse()
		{
		}

		private ServiceResponse(bool isSuccess, [CanBeNull] TPayload payload, [CanBeNull] string errorMessage)
			: base(isSuccess, errorMessage)
		{
			_payload = payload;
		}

		[NotNull]
		public static ServiceResponse<TPayload> Success(TPayload payload, [CanBeNull] string errorMessage = null)
			=> new ServiceResponse<TPayload>(true, payload, errorMessage);

		[NotNull]
		public static ServiceResponse<TPayload> Failure([CanBeNull] TPayload payload = default, [CanBeNull] string errorMessage = null)
			=> new ServiceResponse<TPayload>(false, payload, errorMessage);
	}
}