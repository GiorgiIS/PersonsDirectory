using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TBCPersonsDirectory.Common
{
    public static class ImageChecker
    {
        public static bool IsValidImage(byte[] bytes)
        {
            return GetImageFormat(bytes) != ValidImageFormat.unknown;
        }
        public static ValidImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");               // BMP
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon
            var jpeg3 = new byte[] { 255, 216, 255, 226 };         // jpg
            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ValidImageFormat.bmp;
            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ValidImageFormat.png;
            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ValidImageFormat.jpeg;
            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ValidImageFormat.jpeg;
            if (jpeg3.SequenceEqual(bytes.Take(jpeg3.Length)))
                return ValidImageFormat.jpeg;
            return ValidImageFormat.unknown;
        }
    }

    public enum ValidImageFormat
    {
        bmp,
        png,
        jpeg,
        unknown
    }
}
