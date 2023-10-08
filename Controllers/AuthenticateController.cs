using AstraLicenceManager.Dto;
using AstraLicenceManager.Service;
using Microsoft.AspNetCore.Mvc;

namespace AstraLicenceManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController
    {
        public readonly IAuthenticateService _authenticateService;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(
            IAuthenticateService authenticateService, 
            ILogger<AuthenticateController> logger)
        {
            _authenticateService = authenticateService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<AuthenticateDto> AuthenticateUser(AuthenticateDto item)
        {
            return await _authenticateService.AuthenticateUser(item);
        }
    }
}
