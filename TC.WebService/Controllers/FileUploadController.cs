using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TC.Common.DTO;
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

        public FileUploadController(IFileManager fileManager, IHubContext<SzwagierHub> hubcontext)
        {
            _fileManager = fileManager;
            _hubcontext = hubcontext;
        }    
        [HttpPost("SaveScreenshot")]
        public async Task<IActionResult> SaveScreenshot(IFormFile file)
        {
            try
            {
                string filePath = await _fileManager.SaveFile(file);
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