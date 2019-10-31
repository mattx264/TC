using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.WebService.Models
{
    [Serializable]
    public class SzwagierModel
    {
        public string Name { get; set; }
        public SzwagierType SzwagierType { get; set; }
        public string ConnectionId { get; internal set; }
        public string Location { get; internal set; }
        public string UserId { get; internal set; }
    }
    public enum SzwagierType
    {
        SzwagierConsole = 1,
        SzwagierDashboard,
        SzwagierBrowserExtension
    }
}
