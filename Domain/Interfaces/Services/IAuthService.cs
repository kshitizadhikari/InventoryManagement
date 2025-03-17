using Domain.Entities.DTOs;
using Domain.Entities;

namespace Domain.Interfaces.Services;
public interface IAuthService
{
    Task<ErrorResponse?> RegisterAsync(RegisterUserDTO dto);
    Task<(AuthResponseDTO?, ErrorResponse?)> LoginAsync(LoginUserDTO dto);
    Task<ErrorResponse?> AssignRoleAsync(RoleAssignment item);
}
