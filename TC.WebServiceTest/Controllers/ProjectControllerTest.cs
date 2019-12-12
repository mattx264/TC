using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Controllers;
using TC.WebService.Helpers;
using TC.WebService.ViewModels;
using Xunit;

namespace TC.WebServiceTest.Controllers
{
    public class ProjectControllerTest
    {
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

            var controller = new ProjectController(projectRepositoryMock.Object, userHelper.Object, unitOfWork.Object, userRepository.Object);
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
        [Fact]
        public void Post()
        {
            const string userEmail = "test@test";
            var controller = GetSimplePost(userEmail);

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
            const string userEmail = "test@test";
            var controller = GetSimplePost(userEmail);

            var result = controller.Post(new ProjectCreateViewModel()
            {
                Name = "Test",
                Description = "Test description",
                Domains = "google",
                UsersEmail = userEmail
            });
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(result);
        }
        private ProjectController GetSimplePost(string userEmail)
        {

            var projectRepositoryMock = new Mock<IProjectRepository>();
            var userHelper = new Mock<IUserHelper>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userRepository = new Mock<IUserRepository>();
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
            userHelper.Setup(x => x.GetUser(It.IsAny<ClaimsPrincipal>())).Returns(() => new UserModel() { Id = 1 });

            return new ProjectController(projectRepositoryMock.Object, userHelper.Object, unitOfWork.Object, userRepository.Object);
        }
    }
}
