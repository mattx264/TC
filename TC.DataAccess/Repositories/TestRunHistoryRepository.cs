using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class TestRunHistoryRepository : RepositoryBase<TestRunHistory>, ITestRunHistoryRepository
    {
        public TestRunHistoryRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
