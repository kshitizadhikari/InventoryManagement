using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ErrorResponse?> AssignRoleAsync(RoleAssignment item)
        {
            var user = await _userManager.FindByEmailAsync(item.Email);
            if (user == null)
            {
                return new ErrorResponse
                {
                    Title = "Role Assignment",
                    Message = "User doesn't exist.",
                    ErrorCode = 500
                };
            }

            if (!await _roleManager.RoleExistsAsync(item.Role.ToString()))
            {
                return new ErrorResponse
                {
                    Title = "Role Assignment",
                    Message = "Role doesn't exist",
                    ErrorCode = 500
                };
            }
            await _userManager.AddToRoleAsync(user, item.Role.ToString());
            return null;
        }

        public async Task<(AuthResponseDTO?, ErrorResponse?)> LoginAsync(LoginUserDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return (null, new ErrorResponse
                {
                    Title = "Login Error",
                    Message = "Invalid credentials",
                    ErrorCode = 500
                });
            }

            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var token = new JwtSecurityToken(
                 issuer: jwtSettings["Issuer"],
                 expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
             );


            return (new AuthResponseDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            }, null);
        }

        public async Task<ErrorResponse?> RegisterAsync(RegisterUserDTO dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
            {
                return new ErrorResponse
                {
                    Title = "Registration Error",
                    Message = "User already exists with that email",
                    ErrorCode = 500
                };
            }

            var user = new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return new ErrorResponse
                {
                    Title = "Registration Error",
                    Message = "User registration failed",
                    ErrorCode = 500
                };
            }

            return null;
        }
    }
}
