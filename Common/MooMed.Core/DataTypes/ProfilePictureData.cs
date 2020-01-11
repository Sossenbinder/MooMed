using System.Runtime.Serialization;
using ProtoBuf;

namespace MooMed.Core.DataTypes
{
	[ProtoContract]
	public class ProfilePictureData
	{
		[ProtoMember(1)]
		public byte[] RawData { get; set; }

		[ProtoMember(2)]
		public string FileExtension { get; set; }
	}
}
