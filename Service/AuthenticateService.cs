using AstraLicenceManager.Dto;
using AstraLicenceManager.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AstraLicenceManager.Service
{

    public interface IAuthenticateService
    {
        Task<AuthenticateDto> AuthenticateUser(AuthenticateDto item);       
            
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILogger<AuthenticateService> _logger;
        private readonly ApplicationDbContext _applicationDbContex;

        public AuthenticateService(
            ILogger<AuthenticateService> logger,
            ApplicationDbContext applicationDbContex)
        {
            _logger = logger;
            _applicationDbContex = applicationDbContex;
        }

        public async Task<AuthenticateDto> AuthenticateUser(AuthenticateDto item)
        {           
            
            var user = await _applicationDbContex.Users.FirstOrDefaultAsync(a => a.Email == item.Email && a.Password == item.Password);

            var result = new AuthenticateDto();

            if (user == null)
                {               
                    result.Message= "Korisnički email ili lozinka nisu ispravne !!";
                    return result;
                }

            result.Email = user.Email;
            result.Password = user.Password;          
            result.Token = GenerateToken(result.Email, result.Password);

            return result;
        }

        private string GenerateToken(string email, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)        
            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJNYXJrY0BnbWFpbC5jb20ifQ.RHiZsiQBAXNqc6TxQdbK6uXPnhSM5E91f-qD08TXpbE"));
            SigningCredentials signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeader jwtHeader = new JwtHeader(signingCredential);
            JwtPayload jwtPayload = new JwtPayload(claims);
            JwtSecurityToken token = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }

    }
}
