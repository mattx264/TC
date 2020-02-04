using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;
using System.Linq;
using TC.DataAccess.Repositories.Interfaces;

namespace TC.DataAccess.Repositories
{
    public class ProjectTestConfigRepository : RepositoryBase<ProjectTestConfig>, IProjectTestConfigRepository
    {
        public ProjectTestConfigRepository(TestingCenterDbContext context) : base(context)
        {
        }
        public IList<ProjectTestConfig> GetByProjectId(int projectId)
        {
            return FindAll().Where(x => x.ProjectId == projectId).ToList();
        }
        
    }
}
