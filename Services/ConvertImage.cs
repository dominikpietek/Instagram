using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Instagram.Services
{
    public static class ConvertImage
    {
        public static byte[] ToByteArray(string imagePath)
        {
            Image image = Image.FromFile(imagePath);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }

        public static BitmapImage FromByteArray(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = memoryStream;
                bi.EndInit();
                return bi;
            }
        }

        public static byte[] ImageToBytaArray(BitmapImage bitmapImage)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
