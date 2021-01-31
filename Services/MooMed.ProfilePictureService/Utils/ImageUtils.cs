using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace MooMed.ProfilePictureService.Utils
{
	public static class ImageUtils
	{
		[NotNull]
		public static async Task<Image> ConvertAndScaleRequestImage([NotNull] IAsyncEnumerable<byte[]> imageChunks, [NotNull] string fileExtension)
		{
			await using var stream = new MemoryStream();

			await foreach (var chunk in imageChunks)
			{
				await stream.WriteAsync(chunk);
			}

			stream.Position = 0;

			// Once inStream is filled, convert and scale it
			return ConvertAndScaleRequestImage(stream, fileExtension);
		}

		[CanBeNull]
		private static Image ConvertAndScaleRequestImage([NotNull] Stream inStream, [NotNull] string fileExtension)
		{
			var img = fileExtension.Equals("png") ? Image.Load(inStream, new PngDecoder()) : Image.Load(inStream, new JpegDecoder());

			if (img.Height != 80 || img.Width != 80)
			{
				ScaleImage(img);
			}

			return img;
		}

		private static void ScaleImage([NotNull] Image img)
		{
			img.Mutate(x => x
				.Resize(new ResizeOptions()
				{
					Mode = ResizeMode.Crop,
					Size = new Size(80, 80)
				})
			);
		}
	}
}