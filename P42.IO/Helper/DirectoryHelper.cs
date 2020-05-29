using System.IO;

namespace P42.IO.Helper
{
    internal static class DirectoryHelper
    {
        public static DirectoryInfo CreateDirectory(string path) => System.IO.Directory.CreateDirectory(path);
    }
}