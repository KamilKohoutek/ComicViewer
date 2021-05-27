using System;
using System.Collections.Generic;
using System.IO;

namespace KamilKohoutek.ComicViewer.Core
{
    /// <summary>
    /// Functionalities that every file container should have
    /// </summary>
    public interface IFileContainer : IDisposable
    {
        string FullPath { get; }
        IEnumerable<object> GetFiles();
        Stream Open(object file);
        string GetFilename(object file);
    }
}