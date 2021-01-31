using JetBrains.Annotations;

namespace MooMed.Core.Code.API.Types
{
	public class PostData<T> : QueryData
	{
		[NotNull]
		public T Data { get; }

		public PostData(
			[NotNull] string path,
			[NotNull] T data)
			: base(path)
		{
			Data = data;
		}
	}
}