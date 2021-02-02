using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TC.Common.Consts;
using TC.WebService.Services.Interface;

namespace TC.WebService.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisAdminController : ControllerBase
    {
        private ICacheService _cacheService;

        public RedisAdminController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet("")]
        public IActionResult GetSzwagierModelKeys()
        {
            var result = _cacheService.SearchKeys($"{GenericConsts.SZWAGIER_LIST_KEY}*");
            return Ok(result);
        }
        [HttpGet("/details")]
        public async Task<IActionResult> GetSzwagierModelDetailsAsync(string key)
        {
            var result = await _cacheService.GetSzwagierModelAsync(key);
            return Ok(result);
        }
    }
}

