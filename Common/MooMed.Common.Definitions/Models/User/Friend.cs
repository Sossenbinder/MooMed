using System;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class Friend : IModel
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string UserName { get; set; } = null!;

        [ProtoMember(3)]
        public string Email { get; set; } = null!;

        [ProtoMember(4)]
        public DateTime LastAccessedAt { get; set; }

        [ProtoMember(5)]
        public string ProfilePicturePath { get; set; } = null!;

        [ProtoMember(6)]
        public AccountOnlineState OnlineState { get; set; }
    }
}