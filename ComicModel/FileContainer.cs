using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    /// <summary>
    /// Abstract implementation of the IFileContainer interface
    /// </summary>
    public abstract class FileContainer : IFileContainer
    {
        protected FileContainer(string path) => _fullPath = Path.GetFullPath(path);

        private readonly string _fullPath;
        public string FullPath => _fullPath;

        public abstract string GetFilename(object file);
        public abstract IEnumerable<object> GetFiles();
        public abstract Stream  Open(object file);
        public abstract void Dispose();
    }
}