using JetBrains.Annotations;
using ProtoBuf;
using System;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
	[ProtoInclude(3253, typeof(ServiceResponse))]
	[ProtoInclude(231, typeof(ServiceResponse<LoginResult>))]
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
		private ServiceResponse()
		{
		}

		private ServiceResponse(bool isSuccess, [CanBeNull] string errorMessage)
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