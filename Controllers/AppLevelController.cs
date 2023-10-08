using AstraLicenceManager.Dto;
using AstraLicenceManager.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstraLicenceManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppLevelController : ControllerBase
    {
        public readonly IAppLevelService _appLevelService;
        private readonly ILogger<AppLevelController> _logger;

        public AppLevelController(ILogger<AppLevelController> logger, IAppLevelService appLevelService)
        {
            _logger = logger;
            _appLevelService = appLevelService;
        }

        [HttpGet]
        [Route("GetAppLevel")]
        public async Task<AppLevelDto> GetAppLevel(int id)
        {
            return await _appLevelService.GetAppLevel(id);
        }

        [HttpGet]
        [Route("GetAppLevels")]
        public async Task<List<AppLevelDto>> GetAppLevels()
        {
            return await _appLevelService.GetAppLevels();
        }

        [HttpPost]
        [Route("SaveAppLevel")]
        public async Task<AppLevelDto> SaveApp(AppLevelDto appLevel)
        {
            return await _appLevelService.SaveAppLevel(appLevel);
        }

        [HttpDelete]
        [Route("DeleteAppLevel")]
        public async Task<List<AppLevelDto>> DeleteAppLevel(int id)
        {
            return await _appLevelService.DeleteAppLevel(id);
        }

    }
}
