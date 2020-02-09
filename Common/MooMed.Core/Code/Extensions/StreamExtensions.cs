using System.Collections.Generic;
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

		public static async IAsyncEnumerable<byte[]> ReadAsAsyncEnumerable([NotNull] this Stream stream, int chunkSize = 4096)
		{
			var buffer = new byte[chunkSize];

			int bytesRead;
			do
			{
				bytesRead = await stream.ReadAsync(buffer, 0, chunkSize);

				yield return buffer;
			}
			while (bytesRead != 0);
		}
	}
}
