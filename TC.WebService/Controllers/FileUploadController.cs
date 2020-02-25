using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TC.Common.DTO;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.Entity.Entities;
using TC.WebService.Hubs;
using TC.WebService.Services;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private IFileManager _fileManager;
        private IHubContext<SzwagierHub> _hubcontext;
        private IScreenshotRepository _screenshotRepository;
        private ITestRunResultRepository _testRunResultRepository;
        private IUnitOfWork _unitOfWork;

        public FileUploadController(
            IFileManager fileManager,
            IHubContext<SzwagierHub> hubcontext,
            IScreenshotRepository screenshotRepository,
            ITestRunResultRepository testRunResultRepository,
            IUnitOfWork unitOfWork)
        {
            _fileManager = fileManager;
            _hubcontext = hubcontext;
            _screenshotRepository = screenshotRepository;
            _testRunResultRepository = testRunResultRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("SaveScreenshot")]
        public async Task<IActionResult> SaveScreenshot(IFormFile file)
        {
            try
            {

                string filePath = await _fileManager.SaveFile(file);
                string commandTestGuid = Request.Form["guid"];
                if (String.IsNullOrEmpty(commandTestGuid))
                {
                    throw new Exception($"CommandTestGuid is invalid :{commandTestGuid}");
                }
                var testRunResult=_testRunResultRepository.FindByCondition(x => x.CommandTestGuid == commandTestGuid).FirstOrDefault();
                if (testRunResult == null)
                {
                    throw new Exception($"TestRunResult is not found for commandTestGuid:{commandTestGuid}");
                }
                
                var screenshot= new Screenshot()
                {
                    Path = filePath
                };
                _screenshotRepository.Create(screenshot);
                testRunResult.ScreenshotId = screenshot.Id;
                _unitOfWork.SaveChanges();
               
                await _hubcontext.Clients.Clients(Request.Form["clientId"]).SendAsync("ReciveScreenshot", new TestProgressImageRespons
                {
                    ImagePath = filePath,
                    CommandTestGuid = Request.Form["guid"]
                });
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("SendScreenshot", ex);
            }

        }
    }

}