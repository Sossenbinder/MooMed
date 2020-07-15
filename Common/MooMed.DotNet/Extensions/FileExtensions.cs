using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MooMed.DotNet.Extensions
{
    public static class FileExtensions
    {
        public static async Task AppendAllTextAsync(string filePath, string text)
        {
            var encodedText = Encoding.Unicode.GetBytes(text);

            await using (var sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}
