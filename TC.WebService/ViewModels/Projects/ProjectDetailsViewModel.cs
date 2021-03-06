﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.WebService.ViewModels.Projects
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ProjectDomainViewModel> ProjectDomain { get; set; }
        public IList<UserInProjectViewModel> UserInProject { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime LastTestRunDate { get; set; }
    }
}
