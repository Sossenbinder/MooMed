using JetBrains.Annotations;
using MooMed.Common.Definitions.Database.Entities;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.User
{
    [ProtoContract]
    public class RegisterModel : IModel
    {
        [ProtoMember(1)]
        public string Email { get; set; }

        [ProtoMember(2)]
        public string UserName { get; set; }

        [ProtoMember(3)]
        public string Password { get; set; }

        [ProtoMember(4)]
        public string ConfirmPassword { get; set; }

        [ProtoMember(5)]
        public Language Language { get; set; }
    }
}
