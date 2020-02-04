using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class ConfigProjectTestRepository : RepositoryBase<ConfigProjectTest>, IConfigProjectTestRepository
    {
        public ConfigProjectTestRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
