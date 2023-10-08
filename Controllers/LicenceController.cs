using AstraLicenceManager.Dto;
using AstraLicenceManager.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace AstraLicenceManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LicenceController
    {
        public readonly ILicenceService _licenceService;
        private readonly ILogger<LicenceController> _logger;

        public LicenceController(ILogger<LicenceController> logger, ILicenceService licenceService)
        {
            _logger = logger;
            _licenceService = licenceService;
        }

        [HttpGet]
        [Route("GetLicence")]
        public async Task<LicenceDto> GetLicence(int id)
        {
            return await _licenceService.GetLicence(id);
        }

        [HttpGet]
        [Route("GetLicences")]
        public async Task<List<LicenceDto>> GetLicences()
        {
            return await _licenceService.GetLicences();
        }

        [HttpPost]
        [Route("SaveLicence")]
        public async Task<LicenceDto> SaveLicence(LicenceDto licence)
        {
            return await _licenceService.SaveLicence(licence);
        }

        [HttpDelete]
        [Route("DeleteLicence")]
        public async Task<List<LicenceDto>> DeleteLicence(int id)
        {
            return await _licenceService.DeleteLicence(id);
        }

        [HttpGet]
        [Route("CheckLicence")]
        public async Task<LicenceInfoDto> Check(long appId, long id, string deviceId)
        {
            return await _licenceService.Check(appId, id ,deviceId);
        }
        
    }
}
