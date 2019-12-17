using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;
using TC.Common.Selenium;
using TC.Entity.Entities;

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
        public DbSet<ProjectTestRunConfig> ProjectTestRunConfig { get; set; }
        public DbSet<TestRunConfig> TestRunConfig { get; set; }
        public DbSet<TestRunHistory> TestRunHistory { get; set; }
        public DbSet<TestRunResult> TestRunRestult { get; set; }
        
    }
}
