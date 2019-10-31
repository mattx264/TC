using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>
    {
        public ProjectRepository(TestingCenterDbContext context) : base(context)
        {
            
        }
        public List<Project> GetProjectByUser(string guid)
        {
           return FindAll().Where(x => x.UserGroups.All(x => x.UserModel.Guid.ToString() == guid)).ToList();
        }
    }
}
