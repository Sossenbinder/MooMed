using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Saving
{
    [ProtoContract]
    public class AssetsModel : SessionContextAttachedContainer
    {
        [ProtoMember(1)]
        public int Cash { get; set; }

        [ProtoMember(2)]
        public int Debt { get; set; }

        [ProtoMember(3)]
        public int Equity { get; set; }

        [ProtoMember(4)]
        public int Estate { get; set; }

        [ProtoMember(5)]
        public int Commodities { get; set; }
    }
}