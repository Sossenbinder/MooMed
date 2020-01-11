using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
	[ProtoContract]
	public class AccountIdQuery
	{
		[ProtoMember(1)]
		public int AccountId { get; set; }

		public static implicit operator AccountIdQuery(int accountId) => new AccountIdQuery()
		{
			AccountId = accountId
		};
	}
}
