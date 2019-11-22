using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TC.BrowserEngine.AdminPanel.DataAccess.Models;
using static System.Environment;

namespace TC.BrowserEngine.AdminPanel.DataAccess
{
    public sealed class TCDatabase
    {
        public LiteDatabase db;
        private static readonly Lazy<LiteDatabase>
         lazy =
         new Lazy<LiteDatabase>
             (() => new TCDatabase().db);


        public static LiteDatabase Instance { get { return lazy.Value; } }

        private TCDatabase()
        {
            //AppData\Roaming\myapp
            string path = Path.Combine(Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify), "myapp");

            db = new LiteDatabase(path);

        }


        // Open database (or create if doesn't exist)
        //public Database()
        //    {
        //        string appData = Path.Combine(Environment.GetFolderPath(SpecialFolder.ApplicationData, SpecialFolderOption.DoNotVerify), "myapp");

        //        using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
        //        {
        //            // Get a collection (or create, if doesn't exist)
        //            var col = db.GetCollection<LocalUser>("LocalUser");

        //            // Create your new customer instance
        //            var customer = new LocalUser
        //            {
        //                Name = "John Doe",
        //                Phones = new string[] { "8000-0000", "9000-0000" },
        //                IsActive = true
        //            };

        //            // Insert new customer document (Id will be auto-incremented)
        //            col.Insert(customer);

        //            // Update a document inside a collection
        //            customer.Name = "Joana Doe";

        //            col.Update(customer);

        //            // Index document using document Name property
        //            col.EnsureIndex(x => x.Name);

        //            // Use LINQ to query documents
        //            var results = col.Find(x => x.Name.StartsWith("Jo"));

        //            // Let's create an index in phone numbers (using expression). It's a multikey index
        //            col.EnsureIndex(x => x.Phones, "$.Phones[*]");

        //            // and now we can query phones
        //            var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
        //        }
        //    }
        //}
    }
}