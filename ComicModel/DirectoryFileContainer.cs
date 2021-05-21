using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    public class DirectoryFileContainer : FileContainer
    {
        public DirectoryFileContainer(string path) : base(path) { }

        public override IEnumerable<object> GetFiles() => Directory.GetFiles(FullPath, "*.*", SearchOption.AllDirectories);
        public override Stream Open(object file) => File.OpenRead(file as string);
        public override string GetFilename(object file) => file.ToString();
        public override void Dispose() { }
    }
}