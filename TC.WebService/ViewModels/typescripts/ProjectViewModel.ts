﻿ 
export interface ProjectViewModel { 
  id: number;
  name: string;
  description: string;
  projectDomain: ProjectDomainViewModel[];
  userInProject: UserInProjectViewModel[];
}

