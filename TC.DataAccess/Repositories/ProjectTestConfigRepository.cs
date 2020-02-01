using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class ProjectTestConfigRepository : RepositoryBase<ProjectTestConfig>
    {
        public ProjectTestConfigRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
