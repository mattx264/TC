using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.WebService.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ProjectDomainViewModel> ProjectDomain { get; set; }

    }
    public class ProjectCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Domains { get; set; }
        public string UsersEmail { get; set; }
    }
}
