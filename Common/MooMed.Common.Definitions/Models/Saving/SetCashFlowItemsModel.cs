using System.Collections.Generic;
using MooMed.Common.Definitions.IPC;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Saving
{
    [ProtoContract]
    public class SetCashFlowItemsModel : SessionContextAttachedContainer
    {
        [ProtoMember(1)]
        public List<CashFlowItem> CashFlowItems { get; set; } = null!;
    }
}