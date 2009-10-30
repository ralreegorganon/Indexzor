using System.Collections.Generic;
using System.IO;

namespace Indexzor.Common
{
    public static class DirectoryInfoExtensions
    {
        public static IEnumerable<FileInfo> GetFilesRecursive(this DirectoryInfo dirInfo)
        {
            return GetFilesRecursive(dirInfo, "*.*");
        }
        public static IEnumerable<FileInfo> GetFilesRecursive(this DirectoryInfo dirInfo, string searchPattern)
        {
            foreach (DirectoryInfo di in dirInfo.GetDirectories())
                foreach (FileInfo fi in GetFilesRecursive(di, searchPattern))
                    yield return fi;

            foreach (FileInfo fi in dirInfo.GetFiles(searchPattern))
                yield return fi;
        }
    }
}
