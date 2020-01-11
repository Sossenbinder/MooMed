using ProtoBuf;

namespace MooMed.Common.Definitions.Attributes
{
	public class RequiredProtoMember : ProtoMemberAttribute
	{
		public RequiredProtoMember(int tag) 
			: base(tag)
		{
			IsRequired = true;
		}
	}
}
