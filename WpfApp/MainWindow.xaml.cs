using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ComicModel;
using Ookii.Dialogs.Wpf;
//test
// test
namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private Comic comic = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (cmbPageNumbers.SelectedIndex >= 0 && cmbPageNumbers.SelectedIndex < comic.PageCount - 1)
                cmbPageNumbers.SelectedIndex++;
        }

        private void ShowPage(object sender, SelectionChangedEventArgs e)
        {
            var selection = e.AddedItems;
            if (selection.Count == 0) return;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = comic.GetStream((int)selection[0]);
            bi.EndInit();
            image.Source = bi;
        }

        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if(cmbPageNumbers.SelectedIndex > 0)
                cmbPageNumbers.SelectedIndex--;
        }

        private void ShowOpenFileDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaOpenFileDialog();
            dialog.Title = "Open Archive";
            dialog.Filter = "Comic archives (*.cbz;*.cbr;*.cb7)|*.cbz;*.cbr;*.cb7";
            if(dialog.ShowDialog() == true)
            {
                if (comic != null) comic.Dispose();
                comic = new Comic(new ArchiveFileContainer(dialog.FileName));
                cmbPageNumbers.Items.Clear();
                for(int i = 1; i <= comic.PageCount; i++) cmbPageNumbers.Items.Add(i);
                cmbPageNumbers.SelectedIndex = 0;
            }
        }

        private void ShowOpenFolderDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                if (comic != null) comic.Dispose();
                comic = new Comic(new DirectoryFileContainer(dialog.SelectedPath));
                cmbPageNumbers.Items.Clear();
                for (int i = 1; i <= comic.PageCount; i++) cmbPageNumbers.Items.Add(i);
                cmbPageNumbers.SelectedIndex = 0;
            }
        }

        private void ImageMouseMove(object sender, MouseEventArgs e)
        {
            /*
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(Application.Current.MainWindow);
                scrollViewer.ScrollToVerticalOffset(pos.Y);
                scrollViewer.ScrollToHorizontalOffset(pos.X);
            }
            */
        }
    }
}