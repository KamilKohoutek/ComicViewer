using SharpCompress.Archives;
using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    public class ArchiveFileContainer : FileContainer
    {
        private readonly IArchive archive;

        public ArchiveFileContainer(string path) : base(path) => archive = ArchiveFactory.Open(path);

        public override IEnumerable<object> GetFiles() => archive.Entries;
        public override string GetFilename(object file) => (file as IArchiveEntry).Key;
        public override Stream Open(object file) => (file as IArchiveEntry).OpenEntryStream();
        public override void Dispose() => archive.Dispose();       
    }
}