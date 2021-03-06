﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.ViewModels;

namespace TC.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigProjectTestController : ControllerBase
    {
        #region private
        private IConfigProjectTestRepository _configProjectTestRepository;
        #endregion
        #region constructor
        public ConfigProjectTestController(IConfigProjectTestRepository configProjectTestRepository)
        {
            _configProjectTestRepository = configProjectTestRepository;

        }
        #endregion
        #region GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _configProjectTestRepository.FindAll()
                .Select(x => new ConfigProjectTestViewModel(x))
                .ToList();
            return Ok(result);
        }
        #endregion
    }
}