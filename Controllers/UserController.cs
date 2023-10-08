using AstraLicenceManager.Dto;
using AstraLicenceManager.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstraLicenceManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController:ControllerBase
    {
        public readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<UserDto> GetUser(int id)
        {
            return await _userService.GetUser(id);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<List<UserDto>> GetUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpPost]
        [Route("SaveUser")]
        public async Task<UserDto> SaveUser(UserDto user)
        {
            return await _userService.SaveUser(user);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<List<UserDto>> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}
