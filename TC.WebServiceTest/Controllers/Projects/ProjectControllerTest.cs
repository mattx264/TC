using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TC.DataAccess;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.Entity.Entities.Projects;
using TC.Entity.Entities.User;
using TC.WebService.Controllers;
using TC.WebService.Controllers.Projects;
using TC.WebService.Helpers;
using TC.WebService.ViewModels;
using Xunit;

namespace TC.WebServiceTest.Controllers
{
    public class ProjectControllerTest
    {
        #region Get
        [Theory]
        [MemberData(nameof(GetData))]
        public void Get(List<ProjectDomain> domains, List<UserInProject> userInProject)
        {
            // SETUP
            const int projectid = 1;

            var projectRepositoryMock = new Mock<IProjectRepository>();
            var userHelper = new Mock<IUserHelper>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userRepository = new Mock<IUserRepository>(); 
            var utilHelper = new Mock<IUtilHelper>();

            var controller = new ProjectController(projectRepositoryMock.Object, userHelper.Object, unitOfWork.Object, userRepository.Object, utilHelper.Object);
            projectRepositoryMock.Setup(x => x.GetProjectByUser(It.IsAny<string>(), It.IsAny<int>())).Returns(() =>
            {
               
                return new Project()
                {
                    Id = projectid,
                    Name = "project.Name",
                    ProjectDomains = domains,
                    UserInProject=userInProject
                    
                };
            });
            //EXERCISE
            var result = controller.Get(projectid).Result;

            //VERIFY
            Assert.NotNull(result);

           
        }       

        public static IEnumerable<object[]> GetData()
        {
          
            return new List<object[]>
            {
                new object[] { new List<ProjectDomain>() { new ProjectDomain() { Domain = "google.com" } },null },
              new object[] {  null,new List<UserInProject>() {
                  new UserInProject() { UserModelId=1,UserModel =new UserModel() { Email = "test@test", Id = 1 }
                ,UserProjectStatus=new Entity.Entities.User.UserProjectStatus() { Name="Pending"} } } },
            new object[] { null,null }
          
            };
        }
        #endregion //Get
        #region GetByDomain
        [Fact]
        public void GetByDomain()
        {
            var options = new DbContextOptionsBuilder<TestingCenterDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
           var  context = new TestingCenterDbContext(options);
            context.Projects.Add(new Project() { 
                Name="Test",
                IsActive=true,
                ProjectDomains=new List<ProjectDomain>()
                {
                    new ProjectDomain()
                    {
                        Domain="test.com",
                        IsActive=true
                    }
                },
                UserInProject=new List<UserInProject>()
                {
                    new UserInProject(){
                        IsActive=true,
                        UserModel=new UserModel()
                        {
                            IsActive=true
                            
                        },
                       UserProjectStatus=new UserProjectStatus()
                       {
                           Name="Pending"
                       }
                    }

                }
            });
            context.SaveChanges();
            var user = context.UserModel.FirstOrDefault(x=>x.IsActive);
            var projectRepository = new ProjectRepository(context);
            var userHelper = new Mock<IUserHelper>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userRepository = new Mock<IUserRepository>();
            var utilHelper = new Mock<IUtilHelper>();

            userHelper.Setup(x => x.GetGuid(It.IsAny<ClaimsPrincipal>())).Returns(() => user.Guid.ToString());
           

            var controller = new ProjectController(projectRepository, userHelper.Object, unitOfWork.Object, userRepository.Object, utilHelper.Object);
            
            var result=controller.Get("test.com").GetAwaiter().GetResult();

            Assert.NotNull(result);
        }
        #endregion //GetByDomain
        #region Post
        [Fact]
        public void Post()
        {
            List<string> userEmail = new List<string>() { "test@test" };
            var controller = GetSimplePost(userEmail.First());

            var result = controller.Post(new ProjectCreateViewModel()
            {
                Name = "Test",
                Description = "Test description",
                Domains = "https://www.google.com",
                UsersEmail = userEmail
            });
            Assert.IsType<Microsoft.AspNetCore.Mvc.OkResult>(result);
        }
        [Fact]
        public void Post_BadDomain()
        {
            List<string> userEmail = new List<string>() { "test@test" };
            var controller = GetSimplePost(userEmail.First());

            var result = controller.Post(new ProjectCreateViewModel()
            {
                Name = "Test",
                Description = "Test description",
                Domains = "google",
                UsersEmail = userEmail
            });
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result);
        }
        #endregion
        private ProjectController GetSimplePost(string userEmail)
        {

            var projectRepositoryMock = new Mock<IProjectRepository>();
            var userHelper = new Mock<IUserHelper>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userRepository = new Mock<IUserRepository>();
            var utilHelper = new Mock<IUtilHelper>();

            bool isFirstTimte = true;

            userRepository.Setup(x => x.GetByEmail(userEmail)).Returns(() =>
            {
                if (isFirstTimte)
                {
                    isFirstTimte = false;
                    return null;

                }
                return new UserModel() { Id = 2 };
            });
            userRepository.Setup(x => x.GetByGuid(It.IsAny<string>())).Returns(new Mock<UserModel>().Object);
            userHelper.Setup(x => x.GetUser(It.IsAny<ClaimsPrincipal>())).Returns(() => new UserModel() { Id = 1 });

            return new ProjectController(projectRepositoryMock.Object, userHelper.Object, unitOfWork.Object, userRepository.Object, new UtilHelper());
        }
    }
}
