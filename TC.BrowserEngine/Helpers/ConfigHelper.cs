using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Helpers
{
    public static class ConfigHelper
    {
        /**
         * returns 'https://localhost:44384/'
         */
        public static string GetServerAddress()
    {
#if DEBUG
return "https://localhost:44384/";
#else
     return "prod url";
#endif

        }
    }
}
