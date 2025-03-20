using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services;
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

        var accessToken = await CreateToken(user);

        return (new AuthResponseDTO
        {
            AccessToken = accessToken,
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

    private async Task<string> CreateToken(ApplicationUser user)
    {
        string secretKey = _configuration["JwtSettings:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
    };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role ?? ""));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtSettings:ExpiryMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"]
        };

        var handler = new JsonWebTokenHandler();
        string token = handler.CreateToken(tokenDescriptor);
        return token;
    }
}
