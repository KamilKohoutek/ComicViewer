using System;
using System.IO;
using System.Linq;

namespace ComicModel
{
    public class Comic : IDisposable
    {
        private readonly IFileContainer container;
        private readonly object[] pages;

        public Comic(IFileContainer c, string[] extensionFilter)
        {
            pages = c.GetFiles().Where(f => extensionFilter.Contains(Path.GetExtension(c.GetFilename(f).ToLower()))).OrderBy(f => c.GetFilename(f), new NaturalStringComparer()).ToArray();
            container = c;
        }

        public int PageCount => pages.Length;

        public Stream GetStream(int pageNumber) => container.Open(pages[pageNumber - 1]);
        public string GetName(int pageNumber) => container.GetFilename(pages[pageNumber - 1]);
        public void Dispose() => container.Dispose();
    }
}