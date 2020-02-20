using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories.Interfaces
{
    public interface ITestInfoRepository :IRepositoryBase<TestInfo>
    {
        Task<List<TestInfo>> GetTestInfos(int projectId);

        Task<List<TestInfo>> GetUsersTestInfo(List<int> projectIds);

        TestInfo GetTestInfo(int projectId);
    }
}
