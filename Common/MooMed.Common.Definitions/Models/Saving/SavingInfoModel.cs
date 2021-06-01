using System.Collections.Generic;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Saving
{
	[ProtoContract]
	public class SavingInfoModel : IModel
	{
		[ProtoMember(1)]
		public Currency? Currency { get; set; }

		[ProtoMember(2)]
		public AssetsModel? Assets { get; set; }

		[ProtoMember(3)]
		public BasicSavingInfoModel? BasicSavingInfo { get; set; }

		[ProtoMember(4)]
		public List<CashFlowItem>? FreeFormSavingInfo { get; set; }
	}
}