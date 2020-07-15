using System.Collections.Generic;
using System.IO;

namespace MooMed.DotNet.Extensions
{
	public static class StreamExtensions
	{
		public static byte[] ReadToEnd(this Stream stream)
		{
			var streamAsArr = new byte[stream.Length];

			stream.Read(streamAsArr, 0, (int) stream.Length);

			return streamAsArr;
		}

		public static async IAsyncEnumerable<byte[]> ReadAsAsyncEnumerable(this Stream stream, int chunkSize = 4096)
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
