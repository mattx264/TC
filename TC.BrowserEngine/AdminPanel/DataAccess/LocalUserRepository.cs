using LiteDB;
using System;
using TC.BrowserEngine.AdminPanel.DataAccess.Models;

namespace TC.BrowserEngine.AdminPanel.DataAccess
{
    public class LocalUserRepository
    {
        const string tableName = "LocalUser";
        private LiteDatabase _database;

        public LocalUserRepository()
        {
            _database = TCDatabase.Instance;

        }
        public void SetOrUpdateLocalUser(LocalUser localUser)
        {
            var user = _database.GetCollection<LocalUser>(tableName).FindOne(x => x.Guid == localUser.Guid);
            if (user == null)
            {
                _database.GetCollection<LocalUser>(tableName).Insert(localUser);
            }
            else
            {
                var allActiveUsers = _database.GetCollection<LocalUser>(tableName).Find(x => x.IsActive == true);
                foreach (var item in allActiveUsers)
                {
                    //deactive users ONLY one can be active
                    item.IsActive = false;
                }
                user.IsActive = true;
                user.ModifiedDate = DateTime.Now;
                user.Name = localUser.Name;
                user.Token = localUser.Token;
                _database.GetCollection<LocalUser>(tableName).Update(user);
            }

        }
        internal LocalUser GetCurrentUser()
        {
            var students = _database.GetCollection<LocalUser>(tableName);
            var student =students.FindOne(x => x.IsActive == true);
            return student;
        }
        internal string GetToken()
        {
           
           return GetCurrentUser()?.Token;
        }

        internal void LogoutCurrentUser()
        {
            var user = GetCurrentUser();
            user.IsActive = false;
            user.ModifiedDate =  DateTime.Now;
            _database.GetCollection<LocalUser>(tableName).Update(user);
        }
    }
}
