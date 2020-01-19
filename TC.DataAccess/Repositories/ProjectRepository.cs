using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TC.DataAccess.DatabaseContext;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        public List<Project> GetProjectsByUser(string guid);
        public Project GetProjectByDomain(string guid, string domain);
        public Project GetProjectByUser(string guid, int projectId);
    }
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(TestingCenterDbContext context) : base(context)
        {
        }
        public List<Project> GetProjectsByUser(string guid)
        {
            return FindAll().Where(x => x.UserInProject.Any(x => x.UserModel.Guid.ToString() == guid)).ToList();
        }
        public Project GetProjectByDomain(string guid,string domain)
        {
            return FindAll().FirstOrDefault(x => x.UserInProject.Any(x => x.UserModel.Guid.ToString() == guid) && x.ProjectDomains.FirstOrDefault(x => x.Domain == domain) != null);
        }
        public Project GetProjectByUser(string guid, int projectId)
        {
            return FindAll().First(x => x.UserInProject.Any(x => x.UserModel.Guid.ToString() == guid && x.ProjectId == projectId));
        }
        public IEnumerable<TestInfo> GetProjectTestInfo(Project project, int projectId)
        {
            return project.TestInfos.Where(x => x.ProjectId == projectId);
        }
    }
}
