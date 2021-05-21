using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    class DialogService
    {
        public string OpenFileDialog()
        {
            var dialog = new VistaOpenFileDialog();
            dialog.Title = "Open Archive";
            dialog.Filter = "Comics (*.cbz;*.cbr;*.cb7)|*.cbz;*.cbr;*.cb7|Archives (*.zip;*.rar;*.7z)|*.zip;*.rar;*.7z";
            dialog.Multiselect = false;

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
    }
}
