using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;
using TC.Common.Selenium;
using TC.Entity.Entities;
using TC.Entity.Entities.Projects;

namespace TC.DataAccess.DatabaseContext
{
    public partial class TestingCenterDbContext : DbContext
    {
        public TestingCenterDbContext(DbContextOptions<TestingCenterDbContext> options) : base(options) { }
        public DbSet<ProjectDomain> ProjectDomain { get; set; }
        public DbSet<TestInfo> TestInfo { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserInProject> UserInProject { get; set; }
        public DbSet<ProjectTestConfig> ProjectTestConfig { get; set; }
        public DbSet<ConfigProjectTest> ConfigProjectTest { get; set; }
        public DbSet<TestRunHistory> TestRunHistory { get; set; }
        public DbSet<TestRunResult> TestRunResult { get; set; }
        public DbSet<TestInfoConfig> TestInfoConfig { get; set; }

    }
}
