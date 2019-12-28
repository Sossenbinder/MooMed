using System.Runtime.Serialization;

namespace MooMed.Core.DataTypes
{
	[DataContract]
	public class ProfilePictureData
	{
		[DataMember]
		public byte[] RawData { get; set; }

		[DataMember]
		public string FileExtension { get; set; }
	}
}
