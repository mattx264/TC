using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess;
using TC.DataAccess.Repositories;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestInfoController : ControllerBase
    {
        private TestInfoRepository _testInfoRepository;
        private IUnitOfWork _unitOfWork;

        public TestInfoController(TestInfoRepository testInfoRepository, IUnitOfWork unitOfWork)
        {
            _testInfoRepository = testInfoRepository;
            _unitOfWork = unitOfWork;

        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var testInfo= _testInfoRepository.FindById(id);
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
    }
}