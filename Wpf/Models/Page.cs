using System.Windows.Media.Imaging;

namespace KamilKohoutek.ComicViewer.Wpf.Models
{
    class Page
    {
        public Page(int number) => Number = number;
        public int Number { get; }
        public BitmapImage Image { get; set; } = null;
    }
}