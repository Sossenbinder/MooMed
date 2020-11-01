namespace MooMed.Serialization.Service.Interface
{
	public interface ISerializationService
	{
		string Serialize<T>(T value);

		T Deserialize<T>(string value);
	}
}