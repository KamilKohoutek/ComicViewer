using Ookii.Dialogs.Wpf;

namespace KamilKohoutek.ComicViewer.Wpf.Services
{
    class DialogService
    {
        public string OpenFileDialog()
        {
            var dialog = new VistaOpenFileDialog
            {
                Title = "Open Archive",
                Filter = "Comics (*.cbz;*.cbr;*.cb7)|*.cbz;*.cbr;*.cb7|Archives (*.zip;*.rar;*.7z)|*.zip;*.rar;*.7z",
                Multiselect = false
            };


            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }

        public string FolderBrowserDialog()
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                return dialog.SelectedPath;
            }
            return string.Empty;
        }

        public string SaveFileDialog()
        {
            var dialog = new VistaSaveFileDialog
            {
                Title = "Save Image As",
                Filter = "Images (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                CheckFileExists = false
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }      
    }
}