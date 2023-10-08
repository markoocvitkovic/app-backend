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
    public class AppController:ControllerBase
    {
        public readonly IAppService _appService;
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger, IAppService appService)
        {
            _logger = logger;
            _appService = appService;
        }

        [HttpGet]
        [Route("GetApp")]
        public async Task<AppDto> GetApp(int id)
        {
            return await _appService.GetApp(id);
        }

        [HttpGet]
        [Route("GetApps")]
        public async Task<List<AppDto>> GetApp()
        {
            return await _appService.GetApps();
        }

        [HttpPost]
        [Route("SaveApp")]
        public async Task<AppDto> SaveApp(AppDto app)
        {
            return await _appService.SaveApp(app);
        }

        [HttpDelete]
        [Route("DeleteApp")]
        public async Task<List<AppDto>> DeleteApp(int id)
        {
            return await _appService.DeleteApp(id);
        }
    }
}
