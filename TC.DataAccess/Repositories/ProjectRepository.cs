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
        public List<Project> GetProjectsByUser(string guid)
        {
            return FindAll().Where(x => x.UserInProject.Any(x => x.UserModel.Guid.ToString() == guid)).ToList();
        }
        public Project GetProjectByUser(string guid, int projectId)
        {
            return FindAll().First(x => x.UserInProject.Any(x => x.UserModel.Guid.ToString() == guid && x.Id == projectId));
        }
    }
}
