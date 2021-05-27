using System.Collections.Generic;
using System.IO;

namespace KamilKohoutek.ComicViewer.Core
{
    /// <summary>
    /// Abstract implementation of the IFileContainer interface
    /// </summary>
    public abstract class FileContainer : IFileContainer
    {
        protected FileContainer(string path) => fullPath = Path.GetFullPath(path);

        private readonly string fullPath;
        public string FullPath => fullPath;

        public abstract string GetFilename(object file);
        public abstract IEnumerable<object> GetFiles();
        public abstract Stream  Open(object file);
        public abstract void Dispose();
    }
}