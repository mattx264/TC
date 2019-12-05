using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;
using TC.Entity.Entities;
using TC.Entity.Entities.User;

namespace TC.DataAccess.DatabaseContext
{
    public partial class TestingCenterDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestInfo>().Property(e => e.SeleniumCommands)
              .HasConversion(
              v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
              v => JsonConvert.DeserializeObject<IList<SeleniumCommand>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            //modelBuilder.Entity<UserModel>().HasData(new UserModel
            //{
            //    Id=1,
            //    Guid= Guid.NewGuid(),
            //    Email="test@test",
            //    Password= "AzTnEg5EHn0BVNEo2LFQDryp+gLFqFutciZzqVgcDAA=",
            //    IsActive = true,
            //    CreatedBy = "system",
            //    ModifiedBy = "system",
            //    DateAdded = DateTime.Now,
            //    DateModified = DateTime.Now
            //});
            modelBuilder.Entity<UserProjectStatus>().HasData(new UserProjectStatus()
            {
                Id = 1,
                Name = "Pending",              
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            }, new UserProjectStatus() {
                Id = 2,
                Name = "Accepted",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            }, new UserProjectStatus()
            {
                Id = 3,
                Name = "Rejected",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            }, new UserProjectStatus()
            {
                Id = 4,
                Name = "Deleted",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            const int projectId = 1;
            modelBuilder.Entity<Project>().HasData(new Project
            {
                Id = projectId,
                Name = "Google",
                Description = "",
                ProjectDomains = null,
                UserInProject = null,
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ProjectDomain>().HasData(new ProjectDomain()
            {
                Id=1,
                Domain="google.com",
                ProjectId = projectId,
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ProjectDomain>().HasData(new ProjectDomain()
            {
                Id = 2,
                Domain = "google.pl",
                ProjectId= projectId,
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<TestInfo>().HasData(new TestInfo
            {
                Id=1,
                ProjectId= projectId,
                Name="Search for c# tutorial",
                Description="Using google search find c# tutorial",
                SeleniumCommands=new List<SeleniumCommand>()
                {
                    new SeleniumCommand
                    {
                        OperationId =3, //gotoUrl
                        WebDriverOperationType=WebDriverOperationType.BrowserOperation,
                        Values=new string[]{"https://www.google.com"}
                    }
                },
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now

            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
