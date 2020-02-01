using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TC.DataAccess.Repositories;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigProjectTestController : ControllerBase
    {
        #region private
        private ConfigProjectTestRepository _configProjectTestRepository;
        #endregion
        #region constructor
        public ConfigProjectTestController(ConfigProjectTestRepository configProjectTestRepository)
        {
            _configProjectTestRepository = configProjectTestRepository;

        }
        #endregion
        #region public methods
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _configProjectTestRepository.FindAll()
                .Select(x =>  new ConfigProjectTestViewModel(x))
                .ToList();
            return Ok(result);
        }
        #endregion
    }
}