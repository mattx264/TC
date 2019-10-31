using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.Helpers
{
    public static class ConsoleHelper
    {
        public static string GetValue(string[] args,string name)
        {
            foreach (var item in args)
            {
                var index = item.IndexOf($"{name}:");
                if (index>-1)
                {
                   return item.Substring(name.Length + 1);
                }
            }

            return null;
        }
    }
}
