using AstraLicenceManager.Dto;
using AstraLicenceManager.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AstraLicenceManager.Service
{    
    public interface IUserService
    {
        Task<List<UserDto>> DeleteUser(long id);
        Task<UserDto> GetUser(long id);
        Task<List<UserDto>> GetUsers();
        Task<UserDto> SaveUser(UserDto user);
    }
    public class UserService:IUserService
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(
            ILogger<UserService> logger,
            IMapper mapper,
            ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<UserDto> GetUser(long id)
        {
            var data = await _applicationDbContext.Users.FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<UserDto>(data);
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var data = await _applicationDbContext.Users.ToListAsync();

            return _mapper.Map<List<UserDto>>(data);
        }

        public async Task<UserDto> SaveUser(UserDto userDto)
        {                       

            var user = _mapper.Map<User>(userDto);

            if (userDto.Id == 0)
            {
                string salt = GenerateSalt();
               
                string hashedPassword = HashPassword(userDto.Password, salt);

                user.Password = hashedPassword;

                user.InsertDate = DateTime.Now;
                
                _applicationDbContext.Users.Add(user);

                await _applicationDbContext.SaveChangesAsync();               

                userDto.Id = user.Id;
            }
            else
            {
                var userUpdate = await _applicationDbContext.Users.FirstOrDefaultAsync(a => a.Id == userDto.Id);

                if (userUpdate != null)
                {
                    userUpdate.FirstName = userDto.FirstName;

                    userUpdate.LastName = userDto.LastName;

                    userUpdate.Email = userDto.Email;

                    string salt = GenerateSalt();

                    string hashedPassword = HashPassword(userDto.Password, salt);

                    userUpdate.Password=hashedPassword; 
                    
                    userUpdate.InsertUserId= userDto.InsertUserId;

                    user.UpdateDate = DateTime.Now;

                    userUpdate.UpdateUserId = userDto.UpdateUserId;                    

                    _applicationDbContext.Users.Update(userUpdate);

                    await _applicationDbContext.SaveChangesAsync();

                    }
            }

            var userSave = await GetUser(userDto.Id);

            return _mapper.Map<UserDto>(userSave);
        }

        public async Task<List<UserDto>> DeleteUser(long id)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(a => a.Id == id);

            if (user == null)
            {
                return _mapper.Map<List<UserDto>>(user);
            }
            else
            {
                _applicationDbContext.Users.Remove(user);

                await _applicationDbContext.SaveChangesAsync();
            }

            return _mapper.Map<List<UserDto>>(user);
        }

        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
