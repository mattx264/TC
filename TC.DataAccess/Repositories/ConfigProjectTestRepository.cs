using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class ConfigProjectTestRepository : RepositoryBase<ConfigProjectTest>
    {
        public ConfigProjectTestRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
