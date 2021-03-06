﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public IList<TestRunResult> GetByTestHistoryId(int testHistoryId)
        {
            return FindAll().Where(x => x.TestRunHistoryId == testHistoryId).ToList();
        }
    }
}
