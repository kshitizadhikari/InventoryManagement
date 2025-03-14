using Domain.Entities;
using Domain.Entities.DTOs;

namespace Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<ErrorResponse?> RegisterAsync(RegisterUserDTO dto);
        Task<(AuthResponseDTO?, ErrorResponse?)> LoginAsync(LoginUserDTO dto);
        Task<ErrorResponse?> AssignRoleAsync(RoleAssignment item);
    }
}
