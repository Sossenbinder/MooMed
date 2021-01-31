using JetBrains.Annotations;

namespace MooMed.Core.Code.API.Types
{
	public class QueryData
	{
		[NotNull]
		public string Path { get; }

		public QueryData([NotNull] string path)
		{
			Path = path;
		}
	}
}