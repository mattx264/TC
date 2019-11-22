using LiteDB;
using System;

namespace TC.BrowserEngine.AdminPanel.DataAccess.Models
{
    public class LocalUser : IModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid Guid { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
