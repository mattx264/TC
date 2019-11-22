using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine.AdminPanel.DataAccess.Models
{
    interface IModel
    {
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
