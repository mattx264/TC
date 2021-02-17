using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TC.Common.Selenium;
using TC.Common.Selenium.WebDriverOperation;
using TC.Entity.Entities;
using TC.Entity.Entities.Projects;
using TC.Entity.Entities.User;

namespace TC.DataAccess.DatabaseContext
{
    public partial class TestingCenterDbContext : DbContext
    {
        private const string Salt = "NZsP7NnmfBuYeJrRAKNuVQ==";
        private string PasswordHash(string password)
        {
            // !! IMPORTANT COPY OF THIS CODE IS IN ENTITY FOR SEED USER CREATION

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestInfo>().Property(e => e.SeleniumCommands)
              .HasConversion(
              v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
              v => JsonConvert.DeserializeObject<IList<SeleniumCommand>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                Id = 1,
                Name = "tester",
                UserName ="user name",
                Guid = Guid.NewGuid(),
                Email = "test@test",
                Password = PasswordHash("test123"),
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });

            modelBuilder.Entity<ConfigProjectTest>().Property(c => c.Type).HasConversion<int>();
            modelBuilder.Entity<UserProjectStatus>().HasData(new UserProjectStatus()
            {
                Id = 1,
                Name = "Pending",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            }, new UserProjectStatus()
            {
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
                Description = "Google",
                ProjectDomains = null,               
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<UserInProject>().HasData(new UserInProject()
            {
                Id=1,
                ProjectId= projectId,
                UserModelId=1,
                UserProjectStatusId=2,
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ProjectDomain>().HasData(new ProjectDomain()
            {
                Id = 1,
                Domain = "google.com",
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
                ProjectId = projectId,
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<TestInfo>().HasData(new TestInfo
            {
                Id = 1,
                ProjectId = projectId,
                Name = "Search for c# tutorial",
                Description = "Using google search find c# tutorial",
                SeleniumCommands = new List<SeleniumCommand>()
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
            modelBuilder.Entity<ConfigProjectTest>().HasData(new ConfigProjectTest()
            {
                Id = 1,
                Name = "Take Screenshot After Every Command",
                Description = "It will take a screenshot after every command.",
                Type = Entity.Entities.ConfigProjectTest.ConfigProjectTestEnum.Boolean,
                DefaultValue = "false",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ConfigProjectTest>().HasData(new ConfigProjectTest()
            {
                Id = 2,
                Name = "Monitoring Http Calls",
                Description = "Test will monitor every http call.",
                Type = Entity.Entities.ConfigProjectTest.ConfigProjectTestEnum.Boolean,
                DefaultValue = "false",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ConfigProjectTest>().HasData(new ConfigProjectTest()
            {
                Id = 3,
                Name = "Continue After Command Failure",
                Description = "Test will continue even if a command will fail.",
                Type = Entity.Entities.ConfigProjectTest.ConfigProjectTestEnum.Boolean,
                DefaultValue = "true",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ConfigProjectTest>().HasData(new ConfigProjectTest()
            {
                Id = 4,
                Name = "Wait for network call finish",
                Description = "Every step is waiting for all network e.g. api xhr, images,etc. to finish before go to next command",
                Type = Entity.Entities.ConfigProjectTest.ConfigProjectTestEnum.Boolean,
                DefaultValue = "true",
                IsActive = true,
                CreatedBy = "system",
                ModifiedBy = "system",
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            });
            modelBuilder.Entity<ProjectTestConfig>().HasData(new ProjectTestConfig()
            {
                Id = 1,
                ProjectId = projectId,
                ConfigProjectTestId = 1,
                Value = "true",
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
