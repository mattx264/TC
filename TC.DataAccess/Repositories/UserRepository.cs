using System;
using System.Linq;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<UserModel>
    {
        public UserRepository(TestingCenterDbContext context) : base(context)
        {
        }
        public UserModel Login(string email, string password)
        {
            return context.UserModel.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public UserModel GetByGuid(string guidString)
        {
            Guid guid = new Guid(guidString);
            return context.UserModel.FirstOrDefault(x => x.Guid == guid);
        }
    }

}
