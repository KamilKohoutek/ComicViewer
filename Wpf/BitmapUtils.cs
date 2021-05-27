using System.IO;
using System.Windows.Media.Imaging;

namespace KamilKohoutek.ComicViewer.Wpf
{
    public static class BitmapUtils
    {
        public static BitmapImage CreateBitmapImage(Stream stream)
        {
            var bi = new BitmapImage();
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                ms.Position = 0;
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
            }
            return bi;
        }

        public static void WriteBitmapImageToFile(BitmapImage image, string filename)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
    }
}
