using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TC.BrowserEngine.Helpers
{
    public static class FileHelper
    {
        public static string GetRootPath()
        {
            var path= Directory.GetCurrentDirectory();
#if DEBUG
            path=path.Replace(@"\bin\Debug\netcoreapp2.2", "");
#endif
            return path;
        }
    }
}
