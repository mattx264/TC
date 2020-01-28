using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public interface ITestRunHistoryRepository : IRepositoryBase<TestRunHistory>
    {
      
    }
    public class TestRunHistoryRepository : RepositoryBase<TestRunHistory>, ITestRunHistoryRepository
    {
        public TestRunHistoryRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
