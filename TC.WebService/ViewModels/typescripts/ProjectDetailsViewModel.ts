 
export interface ProjectDetailsViewModel { 
  id: number;
  name: string;
  description: string;
  projectDomain: ProjectDomainViewModel[];
  userInProject: UserInProjectViewModel[];
  dateModified: Date;
  modifiedBy: string;
  lastTestRunDate: Date;
}

