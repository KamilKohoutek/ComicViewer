using System;
using System.IO;
using System.Linq;

namespace ComicModel
{
    public class Comic : IDisposable
    {
        private static readonly string[] ImageExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };
        private static readonly NaturalStringComparer FilenameComparer = new NaturalStringComparer();

        private readonly IFileContainer container;
        private readonly object[] pages;

        public Comic(IFileContainer c)
        {
            container = c;
            pages = container.GetFiles().Where(f => ImageExtensions.Contains(Path.GetExtension(c.GetFilename(f).ToLower()))).OrderBy(f => c.GetFilename(f), FilenameComparer).ToArray();
        }

        public int PageCount => pages.Length;

        public Stream GetStream(int pageNumber) => container.Open(pages[pageNumber - 1]);
        public string GetName(int pageNumber) => container.GetFilename(pages[pageNumber - 1]);
        public void Dispose() => container.Dispose();
    }
}