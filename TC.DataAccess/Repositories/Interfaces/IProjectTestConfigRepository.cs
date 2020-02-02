﻿using System;
using System.Collections.Generic;
using System.Text;
using TC.Entity.Entities;

namespace TC.DataAccess.Repositories.Interfaces
{
    public interface IProjectTestConfigRepository : IRepositoryBase<ProjectTestConfig>
    {
        ProjectTestConfig GetByProjectId(int projectId);
    }
}
