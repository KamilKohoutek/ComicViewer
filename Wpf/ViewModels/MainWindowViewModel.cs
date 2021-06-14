using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using KamilKohoutek.ComicViewer.Core;
using KamilKohoutek.ComicViewer.Wpf.Models;
using KamilKohoutek.ComicViewer.Wpf.Services;

using MvvmHelpers;
using MvvmHelpers.Commands;

namespace KamilKohoutek.ComicViewer.Wpf.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private readonly DialogService dialogService;
        private OrderedFileContainerReader comic = null;

        public MainWindowViewModel()
        {
            dialogService = new DialogService();
            Pages = new ObservableCollection<Page>();
            OpenFileDialogCommand = new Command(OnOpenFileDialog);
            FolderBrowserDialogCommand = new Command(OnFolderBrowserDialog);
            NextPageCommand = new Command(OnNextPageCommand);
            PreviousPageCommand = new Command(OnPreviousPageCommand);
            FirstPageCommand = new Command(OnFirstPageCommand);
            LastPageCommand = new Command(OnLastPageCommand);
        }

        public ObservableCollection<Page> Pages { get; }

        private Page _selectedPage;
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                //if (value == null || value == _selectedPage) return;
                if (value.Image == null)
                    value.Image = BitmapUtils.CreateBitmapImage(comic.GetStream(value.Number - 1));

                DisplayedImage = value.Image;
                SetProperty(ref _selectedPage, value);
            }
        }

        private BitmapImage _displayedImage;
        public BitmapImage DisplayedImage
        {
            get => _displayedImage;
            set => SetProperty(ref _displayedImage, value);
        }

        public ICommand OpenFileDialogCommand { get; }
        public ICommand FolderBrowserDialogCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand FirstPageCommand { get; }
        public ICommand LastPageCommand { get; }


        private void OnNextPageCommand(object obj)
        {
            if(comic != null && SelectedPage.Number < comic.FileCount)
                SelectedPage = Pages[SelectedPage.Number];
        }

        private void OnPreviousPageCommand(object obj)
        {
            if (comic != null && SelectedPage.Number > 1)
                SelectedPage = Pages[SelectedPage.Number - 2];
        }

        private void OnFirstPageCommand(object obj)
        {
            if (comic != null && SelectedPage.Number > 1)
                SelectedPage = Pages[0];
        }
        private void OnLastPageCommand(object obj)
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
                comic = null;
                return;
            }

            if (this.comic != null)
            {
                this.comic.Dispose();
                Pages.Clear();
                GC.Collect();
            }

            for (int i = 0; i < comic.FileCount; i++)
                Pages.Add(new Page(i + 1));

            this.comic = comic;

            SelectedPage = Pages[0];
        }
    }
}