using System;

namespace TC.DataAccess
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
