using System;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class Account : IModel
	{
		public int Id { get; set; }

		public string UserName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public bool EmailConfirmed { get; set; }

		public string PasswordHash { get; set; } = null!;

		public DateTime LastAccessedAt { get; set; }

		public DateTimeOffset? LockoutEnd { get; set; }

		public string ProfilePicturePath { get; set; } = null!;

		public int AccessFailedCount { get; set; }

		public string ConcurrencyStamp { get; set; } = null!;

		public bool LockoutEnabled { get; set; }

		public string NormalizedEmail { get; set; } = null!;

		public string NormalizedUserName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public bool PhoneNumberConfirmed { get; set; }

		public string SecurityStamp { get; set; } = null!;

		public bool TwoFactorEnabled { get; set; }
	}

	public static class AccountExtensions
	{
		[NotNull]
		public static string IdAsKey([NotNull] this Account account)
		{
			return $"a-{account.Id}";
		}
	}

	[ProtoContract]
	public class DateTimeOffsetSurrogate
	{
		[ProtoMember(1)]
		public string DateTimeString { get; set; } = null!;

		public static implicit operator DateTimeOffsetSurrogate(DateTimeOffset? value)
		{
			if (!value.HasValue)
			{
				return null;
			}

			return new DateTimeOffsetSurrogate
			{
				DateTimeString = value.Value.ToString("u")
			};
		}

		public static implicit operator DateTimeOffset?(DateTimeOffsetSurrogate value)
		{
			if (value.DateTimeString == null)
			{
				return null;
			}

			return DateTimeOffset.Parse(value.DateTimeString);
		}
	}
}