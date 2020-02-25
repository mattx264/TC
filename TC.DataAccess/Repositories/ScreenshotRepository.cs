using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public class ScreenshotRepository : RepositoryBase<Screenshot>, IScreenshotRepository
    {
        public ScreenshotRepository(TestingCenterDbContext context) : base(context)
        {
        }
    }
}
