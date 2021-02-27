using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.Controllers.Test;
using TC.WebService.Services.Interface;
using TC.WebService.ViewModels.Projects;
using Xunit;

namespace TC.WebServiceTest.Controllers.Tests
{
    public class TestInfoConfigControllerTests
    {
        private Mock<IConfigProjectTestRepository> _configProjectTestRepository;
        private Mock<IProjectTestConfigService> _projectTestConfigService;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ITestInfoConfigRepository> _testInfoConfigRepository;

        [Fact]
        public void Post_Can()
        {
            var controller = GetController();
            var resutl = controller.Post(new List<TestInfoConfigViewModel>()
            {
                new TestInfoConfigViewModel()
            });
        }

        [Fact]
        public void Get_Can()
        {
            var controller = GetController();
            controller.Get(999);
        }

        private TestInfoConfigController GetController()
        {
            _testInfoConfigRepository = new Mock<ITestInfoConfigRepository>();
            _configProjectTestRepository = new Mock<IConfigProjectTestRepository>();
            _projectTestConfigService = new Mock<IProjectTestConfigService>();
            _projectTestConfigService.Setup(x => x.GetTestConfigByTestId(999)).Returns(new List<TestInfoConfigViewModel>()
            {
                new TestInfoConfigViewModel(){}
            });
            _unitOfWork = new Mock<IUnitOfWork>();
          
            return new TestInfoConfigController(_testInfoConfigRepository.Object,
                _configProjectTestRepository.Object,
                _projectTestConfigService.Object,
                _unitOfWork.Object);
        }
    }
}
