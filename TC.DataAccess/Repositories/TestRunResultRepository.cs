using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class TestRunResultRepository : RepositoryBase<TestRunResult>, ITestRunResultRepository
    {
        public TestRunResultRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
