using System.IO;
using MooMed.Serialization.Service.Interface;
using ProtoBuf;

namespace MooMed.Serialization.Service
{
	public class ProtobufSerializationService : ISerializationService
	{
		public string Serialize<T>(T value)
		{
			using var serializationStream = new MemoryStream();

			Serializer.Serialize(serializationStream, value);

			using var streamReader = new StreamReader(serializationStream);
			return streamReader.ReadToEnd();
		}

		public T Deserialize<T>(string value)
		{
			using var serializationStream = new MemoryStream();

			using var streamWriter = new StreamWriter(serializationStream);
			streamWriter.Write(value);

			return Serializer.Deserialize<T>(serializationStream);
		}
	}
}