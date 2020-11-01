using System.Text.Json;
using MooMed.Serialization.Serializers;
using MooMed.Serialization.Service.Interface;

namespace MooMed.Serialization.Service
{
	public class JsonSerializationService : ISerializationService
	{
		private readonly JsonSerializerOptions _jsonSerializerOptions;

		public JsonSerializationService()
		{
			_jsonSerializerOptions = new JsonSerializerOptions();
			_jsonSerializerOptions.Converters.Add(new SessionContextConverter());
		}

		public string Serialize<T>(T value)
		{
			return JsonSerializer.Serialize(value, _jsonSerializerOptions);
		}

		public T Deserialize<T>(string value)
		{
			return JsonSerializer.Deserialize<T>(value, _jsonSerializerOptions);
		}
	}
}