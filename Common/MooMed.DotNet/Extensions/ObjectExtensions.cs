namespace MooMed.DotNet.Extensions
{
	public static class ObjectExtensions
	{
		public static string ToJsonString(this object objToConvert)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(objToConvert);
		}
	}
}
