using System;
using System.IO;
using System.Linq;

namespace KamilKohoutek.ComicViewer.Core
{
    /// <summary>
    /// Reads
    /// </summary>
    public class OrderedFileContainerReader : IDisposable
    {
        private readonly IFileContainer container;
        private readonly object[] files;

        public OrderedFileContainerReader(IFileContainer c, string[] extensionFilter)
        {
            files = c.GetFiles().Where(f => extensionFilter.Contains(Path.GetExtension(c.GetFilename(f).ToLower()))).OrderBy(f => c.GetFilename(f), new NaturalStringComparer()).ToArray();
            container = c;
        }

        public int FileCount => files.Length;

        public Stream GetStream(int i) => container.Open(files[i]);
        public string GetName(int i) => container.GetFilename(files[i]);
        public void Dispose() => container.Dispose();
    }
}