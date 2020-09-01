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

		public bool IsFailure => !IsSuccess;

		protected ServiceResponseBase()
		{
		}

		protected ServiceResponseBase(bool isSuccess, string? errorMessage)
		{
			IsSuccess = isSuccess;
			ErrorMessage = new ServiceErrorMessage(errorMessage);
		}
	}

	[ProtoContract]
	public class ServiceResponse : ServiceResponseBase
	{
		// Implicitly used by protobuf-net, so the lib can use this instead of having to fallback onto the non-parameterless constructor
		[UsedImplicitly]
		protected ServiceResponse()
		{
		}

		public ServiceResponse(bool isSuccess, string? errorMessage = null)
			: base(isSuccess, errorMessage)
		{
		}

		[NotNull]
		public static ServiceResponse Success(string? errorMessage = null)
			=> new ServiceResponse(true, errorMessage);

		[NotNull]
		public static ServiceResponse Failure(string? errorMessage = null)
			=> new ServiceResponse(false, errorMessage);

		[NotNull]
		public static ServiceResponse<TPayload> Success<TPayload>([NotNull] TPayload payload, string? errorMessage = null)
			=> new ServiceResponse<TPayload>(true, payload, errorMessage);

		[NotNull]
		public static ServiceResponse<TPayload> Failure<TPayload>([NotNull] TPayload payload, string? errorMessage = null)
			=> new ServiceResponse<TPayload>(false, payload, errorMessage);
	}

	[ProtoContract]
	// Simple base class to transport the result of a backend task to the frontend and provide a way to check whether call was successful
	public class ServiceResponse<TPayload> : ServiceResponseBase
	{
		[ProtoMember(3)]
		public TPayload PayloadOrNull { get; }

		[NotNull]
		public TPayload PayloadOrFail
		{
			get
			{
				if (PayloadOrNull == null)
				{
					throw new NullReferenceException();
				}

				return PayloadOrNull;
			}
		}

		[UsedImplicitly]
		private ServiceResponse()
		{
		}

		public ServiceResponse(bool isSuccess, [CanBeNull] TPayload payload = default, string? errorMessage = null)
			: base(isSuccess, errorMessage)
		{
			PayloadOrNull = payload;
		}

		[NotNull]
		public static ServiceResponse<TPayload> Success(TPayload payload, string? errorMessage = null)
			=> new ServiceResponse<TPayload>(true, payload, errorMessage);

		[NotNull]
		public static ServiceResponse<TPayload> Failure([CanBeNull] TPayload payload = default, string? errorMessage = null)
			=> new ServiceResponse<TPayload>(false, payload, errorMessage);
	}
}