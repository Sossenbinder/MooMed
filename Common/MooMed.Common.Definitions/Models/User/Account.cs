using JetBrains.Annotations;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;
using System;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class Account : IModel
	{
		[ProtoMember(1)]
		public int Id { get; set; }

		[ProtoMember(2)]
		public string UserName { get; set; } = null!;

		[ProtoMember(3)]
		public string Email { get; set; } = null!;

		[ProtoMember(4)]
		public bool EmailConfirmed { get; set; }

		[ProtoMember(5)]
		public string PasswordHash { get; set; } = null!;

		[ProtoMember(6)]
		public DateTime LastAccessedAt { get; set; }

		[ProtoMember(7)]
		public DateTimeOffset? LockoutEnd { get; set; }

		[ProtoMember(8)]
		public string ProfilePicturePath { get; set; } = null!;

		[ProtoMember(9)]
		public int AccessFailedCount { get; set; }

		[ProtoMember(10)]
		public string ConcurrencyStamp { get; set; } = null!;

		[ProtoMember(11)]
		public bool LockoutEnabled { get; set; }

		[ProtoMember(12)]
		public string NormalizedEmail { get; set; } = null!;

		[ProtoMember(13)]
		public string NormalizedUserName { get; set; } = null!;

		[ProtoMember(14)]
		public string PhoneNumber { get; set; } = null!;

		[ProtoMember(15)]
		public bool PhoneNumberConfirmed { get; set; }

		[ProtoMember(16)]
		public string SecurityStamp { get; set; } = null!;

		[ProtoMember(17)]
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
		public long DateTimeTicks { get; set; }

		[ProtoMember(2)]
		public short OffsetMinutes { get; set; }

		public static implicit operator DateTimeOffsetSurrogate(DateTimeOffset value)
		{
			return new DateTimeOffsetSurrogate
			{
				DateTimeTicks = value.Ticks,
				OffsetMinutes = (short)value.Offset.TotalMinutes
			};
		}

		public static implicit operator DateTimeOffset(DateTimeOffsetSurrogate value)
		{
			return new DateTimeOffset(value.DateTimeTicks, TimeSpan.FromMinutes(value.OffsetMinutes));
		}
	}
}