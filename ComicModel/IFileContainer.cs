using System;
using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    public interface IFileContainer : IDisposable
    {
        IEnumerable<object> GetFiles();
        Stream Open(object file);
        string GetFilename(object file);
    }
}