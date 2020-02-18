using System;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
    [ProtoContract]
    public class AccountValidationTokenData
    {
        [ProtoMember(1)]
        public int AccountId { get; set; }

        [ProtoMember(2)]
        public Guid ValidationGuid { get; set; }
    }
}
