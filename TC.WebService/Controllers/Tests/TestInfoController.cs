using Microsoft.AspNetCore.Mvc;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestInfoController : ControllerBase
    {
        private ITestInfoRepository _testInfoRepository;
        private IUnitOfWork _unitOfWork;

        public TestInfoController(ITestInfoRepository testInfoRepository, IUnitOfWork unitOfWork)
        {
            _testInfoRepository = testInfoRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var testInfo = _testInfoRepository.FindById(id);
            return Ok(new TestInfoViewModel()
            {
                Commands = testInfo.SeleniumCommands,
                Description = testInfo.Description,
                Id = testInfo.Id,
                Name = testInfo.Name,
                ProjectId = testInfo.ProjectId
            });
        }
        [HttpPut]
        public IActionResult Put(TestInfoViewModel viewModel)
        {
            var testInfo = _testInfoRepository.FindById(viewModel.Id);
            testInfo.SeleniumCommands = viewModel.Commands;
            testInfo.Description = viewModel.Description;
            testInfo.Name = viewModel.Name;
            _unitOfWork.SaveChanges();
            return Ok();

        }
        [HttpDelete]
        public IActionResult Delete(int testInfoId)
        {
            var testInfo = _testInfoRepository.FindById(testInfoId);
            _testInfoRepository.Delete(testInfo);
            _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}