using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    public class DirectoryFileContainer : IFileContainer
    {
        private readonly string path;

        public DirectoryFileContainer(string path) => this.path = path;

        public IEnumerable<object> GetFiles() => Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        public Stream Open(object file) => File.OpenRead(file as string);
        public string GetFilename(object file) => file.ToString();
        public void Dispose() { }
    }
}