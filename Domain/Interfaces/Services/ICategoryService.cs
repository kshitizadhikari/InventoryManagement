using Domain.Entities;
using Domain.Entities.DTOs;

namespace Domain.Interfaces.Services;
public interface ICategoryService
{
    Task<(CategoryDTO?, ErrorResponse?)> CreateAsync(CategoryDTO dto);
    Task<List<CategoryDTO>> GetAllAsync();
    Task<(CategoryDTO?, ErrorResponse?)> GetByIdAsync(string id);
    Task<(CategoryDTO?, ErrorResponse?)> UpdateAsync(CategoryDTO dto);
}
