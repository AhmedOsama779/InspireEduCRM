using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InspireEduCRM.Application.DTOs.Auth;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InspireEduCRM.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public AuthService(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email== request.Email);
            if (user == null) return null;

            // Verify password against stored hash
            bool validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!validPassword) return null;

            var token = GenerateJwtToken(user.Id,user.Name,user.Email,user.Role.ToString());

            return new LoginResponse
            {
                Token = token,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
            };
        }
        public string GenerateJwtToken(int userId, string name, string email, string role) 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.Role,role)
            };
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiryMinutes = double.Parse(_config["Jwt:ExpiryMinutes"]!);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience:_config["Jwt:Audience"],
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
