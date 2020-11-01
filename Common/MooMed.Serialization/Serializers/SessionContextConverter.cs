using System;
using System.Text.Json;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.Session.Interface;

namespace MooMed.Serialization.Serializers
{
	public class SessionContextConverter : System.Text.Json.Serialization.JsonConverter<ISessionContext>
	{
		public override ISessionContext Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonSerializer.Deserialize<SessionContext>(ref reader);
		}

		public override void Write(Utf8JsonWriter writer, ISessionContext value, JsonSerializerOptions options)
		{
			JsonSerializer.Serialize(writer, value, typeof(SessionContext));
		}
	}
}