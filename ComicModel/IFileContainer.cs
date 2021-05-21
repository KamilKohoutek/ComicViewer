using System;
using System.Collections.Generic;
using System.IO;

namespace ComicModel
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IFileContainer : IDisposable
    {
        string FullPath { get; }
        IEnumerable<object> GetFiles();
        Stream Open(object file);
        string GetFilename(object file);
    }
}