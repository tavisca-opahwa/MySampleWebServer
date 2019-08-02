using System.Collections.Generic;
using System.IO;

namespace MySampleWebServer
{
    public class FileHandler
    {
        private static HashSet<string> _defaultFile = new HashSet<string>
        {
            "index.html",
            "index.htm",
            "default.html",
            "default.htm"
        };
        public static FileInfo GetDefaultFile(FileInfo[] files)
        {
            foreach (FileInfo ff in files)
            {
                string n = ff.Name;
                if (_defaultFile.Contains(n) == true)
                    return ff;
            }
            return null;
        }
        public static bool IsValidFile(FileInfo file) => (file.Exists && file.Extension.Contains("."));
        public static bool IsValidDirectory(DirectoryInfo dir) => (dir.Exists);

    }
}
