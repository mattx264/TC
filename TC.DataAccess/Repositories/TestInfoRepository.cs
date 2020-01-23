using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class TestInfoRepository : RepositoryBase<TestInfo>
    {
        public TestInfoRepository(TestingCenterDbContext context) : base(context)
        {
        }
        public Task<List<TestInfo>> GetTestInfos(int projectId)
        {
            return  FindAll().Where(x => x.ProjectId == projectId).ToListAsync();
        }
       
    }

}
