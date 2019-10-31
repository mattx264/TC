using System;
using System.Collections.Generic;
using System.Text;
using TC.DataAccess.DatabaseContext;

namespace TC.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private TestingCenterDbContext _context;

        public UnitOfWork(TestingCenterDbContext context)
        {
            _context = context;
        }
        public int SaveChanges()
        {

            return _context.SaveChanges(this); ;
        }
    }

}
