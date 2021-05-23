using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using KamilKohoutek.ComicViewer.Core;
using KamilKohoutek.ComicViewer.Wpf.Commands;
using KamilKohoutek.ComicViewer.Wpf.Models;
using KamilKohoutek.ComicViewer.Wpf.Services;

namespace KamilKohoutek.ComicViewer.Wpf.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly WindowsDialogService dialogService;
        private OrderedFileContainerReader comic = null;

        public MainWindowViewModel()
        {
            dialogService = new WindowsDialogService();
            Pages = new ObservableCollection<Page>();
            OpenFileDialogCommand = new DelegateCommand(OnOpenFileDialog);
            FolderBrowserDialogCommand = new DelegateCommand(OnFolderBrowserDialog);
            NextPageCommand = new DelegateCommand(NextPageCommand_Execute);
            PreviousPageCommand = new DelegateCommand(PreviousPageCommand_Execute);
            FirstPageCommand = new DelegateCommand(FirstPageCommand_Execute);
            LastPageCommand = new DelegateCommand(LastPageCommand_Execute);
        }

        public ObservableCollection<Page> Pages { get; }

        private Page _selectedPage;
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (value == null || value == _selectedPage) return;
                if (value.Image == null)
                    value.Image = CreateBitmapImage(comic.GetStream(value.Number - 1));
  
                DisplayedImage = value.Image;
                _selectedPage = value;
                OnPropertyChanged("SelectedPage");
            }
        }

        private BitmapImage _displayedImage;
        public BitmapImage DisplayedImage
        {
            get => _displayedImage;
            set
            {
                if (value != _displayedImage)
                {
                    _displayedImage = value;
                    OnPropertyChanged("DisplayedImage");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ICommand OpenFileDialogCommand { get; }
        public ICommand FolderBrowserDialogCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand FirstPageCommand { get; }
        public ICommand LastPageCommand { get; }

        private void NextPageCommand_Execute(object obj)
        {
            if(comic != null && SelectedPage.Number < comic.FileCount)
                SelectedPage = Pages[SelectedPage.Number];
        }

        private void PreviousPageCommand_Execute(object obj)
        {
            if (comic != null && SelectedPage.Number > 1)
                SelectedPage = Pages[SelectedPage.Number - 2];
        }

        private void FirstPageCommand_Execute(object obj)
        {
            if (comic != null && SelectedPage.Number > 1)
                SelectedPage = Pages[0];
        }
        private void LastPageCommand_Execute(object obj)
        {
            if (comic != null && SelectedPage.Number < comic.FileCount)
                SelectedPage = Pages[Pages.Count - 1];
        }

        private void OnOpenFileDialog(object obj)
        {
            var filename = dialogService.OpenFileDialog();
            if (filename == string.Empty) return;
            OpenComic(new ArchiveFileContainer(filename));
        }

        private void OnFolderBrowserDialog(object obj)
        {
            var path = dialogService.FolderBrowserDialog();
            if (path == string.Empty) return;
            OpenComic(new DirectoryFileContainer(path));
        }

        private void OpenComic(IFileContainer source)
        {
            var comic = new OrderedFileContainerReader(source, new string[] { ".jpg", ".jpeg", ".png", ".bmp" });
            if (comic.FileCount == 0)
            {
                MessageBox.Show("No supported image files have been found in " + source.FullPath, "ComicViewer");
                comic.Dispose();
                return;
            }

            if (this.comic != null)
            {
                this.comic.Dispose();
                Pages.Clear();
            }

            for (int i = 0; i < comic.FileCount; i++)
                Pages.Add(new Page(i + 1));

            this.comic = comic;
            SelectedPage = Pages[0];
        }

        private static BitmapImage CreateBitmapImage(Stream stream)
        {
            var bi = new BitmapImage();
            using(var ms = new MemoryStream())
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
    }
}