using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Saving
{
    [ProtoContract]
    public class BasicSavingInfoModel : SessionContextAttachedContainer
    {
        [ProtoMember(1)]
        public CashFlowItem Income { get; init; }

        [ProtoMember(2)]
        public CashFlowItem Rent { get; init; }

        [ProtoMember(3)]
        public CashFlowItem Groceries { get; init; }
    }
}