using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace KamilKohoutek.ComicViewer.Core
{
    /// <summary>
    /// String comparer for natural sort order. This uses a Windows-specific API for string comparison.
    /// </summary>
    internal class NaturalStringComparer : Comparer<string>
    {
        public override int Compare(string x, string y) => SafeNativeMethods.StrCmpLogicalW(x, y);
        
        [SuppressUnmanagedCodeSecurity]
        private static class SafeNativeMethods
        {
            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
            public static extern int StrCmpLogicalW(string psz1, string psz2);
        }
    }
}