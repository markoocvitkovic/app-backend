using Microsoft.AspNetCore.Mvc;
using AstraLicenceManager.Dto;
using AstraLicenceManager.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AstraLicenceManager.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyController:ControllerBase
    {
        public readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet]
        [Route("GetCompany")]
        public async Task<CompanyDto> GetCompany(int id)
        {
            return await _companyService.GetCompany(id);
        }

        [HttpGet]
        [Route("GetCompanies")]
        public async Task<List<CompanyDto>> GetCompany()
        {
            return await _companyService.GetCompanies();
        }

        [HttpPost]
        [Route("SaveCompany")]
        public async Task<CompanyDto> SaveCompany(CompanyDto company)
        {
            return await _companyService.SaveCompany(company);
        }

        [HttpDelete]
        [Route("DeleteCompany")]
        public async Task<List<CompanyDto>> DeleteCompany(int id)
        {
            return await _companyService.DeleteCompany(id);
        }

    }
}
