using System.IO;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Extensions
{
    public static class FileExtensions
    {
        public static async Task AppendAllTextAsync([NotNull] string filePath,[NotNull] string text)
        {
            var encodedText = Encoding.Unicode.GetBytes(text);

            using (var sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}
