using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using JetBrains.Annotations;

namespace MooMed.Core.Code.Utils
{
    public static class ImageUtils
    {
        [CanBeNull]
        public static Image ConvertAndScaleRequestImage([NotNull] byte[] file)
        {
            Image img = null;

            using (var stream = new MemoryStream(file))
            {
	            var convertedImg = Image.FromStream(stream, true, true);

	            if (convertedImg.Height != 80 || convertedImg.Width != 80)
	            {
		            img = ScaleImage(convertedImg);
	            }

	            return img;
            }
        }

        [NotNull]
        private static Image ScaleImage([NotNull] Image img)
        {
            var oldBmp = new Bitmap(img);

            var bmpTarget = new Bitmap(80, 80);
            bmpTarget.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (var graphics = Graphics.FromImage(bmpTarget))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(oldBmp, 0, 0, 80, 80);
            }

            return bmpTarget;
        }
    }
}