using ProtoBuf;

namespace MooMed.Common.Definitions.IPC
{
	[ProtoContract]
	[ProtoInclude(1, typeof(int))]
	
	public class Primitive<T>
	{
		[ProtoMember(1)]
		public T Value { get; set; }

		public static implicit operator Primitive<T>(T val) => new Primitive<T>()
		{
			Value = val
		};

		public static implicit operator T(Primitive<T> primitive) => primitive.Value;
	}
}
