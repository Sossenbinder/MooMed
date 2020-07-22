using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class Account : IdentityUser<int>, IModel
    {
        public DateTime LastAccessedAt { get; set; }

        public string ProfilePicturePath { get; set; }
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
	    public string DateTimeString { get; set; }

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
