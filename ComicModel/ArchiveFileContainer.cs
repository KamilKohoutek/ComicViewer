using SharpCompress.Archives;
using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    public class ArchiveFileContainer : IFileContainer
    {
        private readonly IArchive archive;

        private ArchiveFileContainer(IArchive archive) => this.archive = archive;
        public ArchiveFileContainer(string path) : this(ArchiveFactory.Open(path)) { }

        public IEnumerable<object> GetFiles() => archive.Entries;
        public string GetFilename(object file) => (file as IArchiveEntry).Key;
        public Stream Open(object file) => (file as IArchiveEntry).OpenEntryStream();
        public void Dispose() => archive.Dispose();       
    }
}