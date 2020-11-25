using System;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Saving
{
    [ProtoContract]
    public class CashFlowItem : IModel
    {
        [ProtoMember(1)]
        public string Name { get; set; } = null!;

        [ProtoMember(2)]
        public Guid Identifier { get; set; }

        [ProtoMember(3)]
        public CashFlowItemType CashFlowItemType { get; set; }

        [ProtoMember(4)]
        public double Amount { get; set; }

        [ProtoMember(5)]
        public CashFlow FlowType { get; set; }
    }
}