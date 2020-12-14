using System.Runtime.Serialization;
using JetBrains.Annotations;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class LoginModel
    {
        [ProtoMember(1)]
        [NotNull]
        public string Email { get; set; } = null!;

        [ProtoMember(2)]
        [NotNull]
        public string Password { get; set; } = null!;

        [ProtoMember(3)]
        public bool RememberMe { get; set; }
    }
}