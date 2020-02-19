using System.Collections.Generic;
using System.Linq;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class TestInfoConfigRepository : RepositoryBase<TestInfoConfig>, ITestInfoConfigRepository
    {
        public TestInfoConfigRepository(TestingCenterDbContext context) : base(context)
        {
        }
        public IList<TestInfoConfig> FindByTestId(int testId)
        {
            return FindAll().Where(x => x.TestInfoId == testId).ToList();
        }
    }
}
