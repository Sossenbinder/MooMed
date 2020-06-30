using System.Collections.Generic;
using MooMed.Common.Definitions.Interface;
using ProtoBuf;

namespace MooMed.Common.Definitions.Models.Portfolio
{
	[ProtoContract]
	public class Portfolio : IModel
	{
		[ProtoMember(1)] 
		public IEnumerable<PortfolioItem> Items { get; set; }
	}
}
