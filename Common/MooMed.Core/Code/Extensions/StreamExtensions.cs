using System.IO;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Extensions
{
	public static class StreamExtensions
	{
		[NotNull]
		public static byte[] ReadToEnd([NotNull] this Stream stream)
		{
			var streamAsArr = new byte[stream.Length];

			stream.Read(streamAsArr, 0, (int) stream.Length);

			return streamAsArr;
		}
	}
}
