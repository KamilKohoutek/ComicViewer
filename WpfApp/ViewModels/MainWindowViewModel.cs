using ComicModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly DialogService dialogService;
        private Comic comic;

        public MainWindowViewModel()
        {
            comic = null;
            dialogService = new DialogService();
            OpenFileDialogCommand = new DelegateCommand(OnOpenFileDialog);
            FolderBrowserDialogCommand = new DelegateCommand(OnFolderBrowserDialog);
            Pages = new ObservableCollection<Page>();
        }

        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand FolderBrowserDialogCommand { get; set; }

        private BitmapImage _displayedImage;
        public BitmapImage DisplayedImage 
        { 
            get => _displayedImage; 
            set
            {
                if(value != _displayedImage)
                {
                    _displayedImage = value;
                    NotifyPropertyChanged("DisplayedImage");
                }
            }
        }

        private ObservableCollection<Page> _pages;
        public ObservableCollection<Page> Pages { get => _pages; set => _pages = value; }

        private Page _selectedPage;
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (value != null && value != _selectedPage)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = comic.GetStream(value.Number);
                    bi.EndInit();
                    DisplayedImage = bi;

                    _selectedPage = value;
                    NotifyPropertyChanged("SelectedPage");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void OnOpenFileDialog(object obj)
        {
            var filename = dialogService.OpenFileDialog();
            if (filename == string.Empty) return;
            if (comic != null) comic.Dispose();
            comic = new Comic(new ArchiveFileContainer(filename), new string[] { ".jpg", ".jpeg", ".png", ".bmp" });
            Pages.Clear();
            for (int i = 1; i <= comic.PageCount; i++)
            {
                Pages.Add(new Page(i));
            }
            SelectedPage = Pages[0];
        }

        private void OnFolderBrowserDialog(object obj)
        {
            var path = dialogService.FolderBrowserDialog();
            if (path == string.Empty) return;
            if (comic != null) comic.Dispose();
            comic = new Comic(new DirectoryFileContainer(path), new string[] { ".jpg", ".jpeg", ".png", ".bmp" });
            Pages.Clear();
            for(int i = 1; i <= comic.PageCount; i++)
            {
                Pages.Add(new Page(i));
            }
            SelectedPage = Pages[0];
        }
    }
}