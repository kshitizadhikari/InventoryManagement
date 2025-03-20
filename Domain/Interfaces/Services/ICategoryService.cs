using Domain.Entities.DTOs;

namespace Domain.Interfaces.Services;
public interface ICategoryService
{
    Task<CategoryDTO> CreateAsync(CategoryDTO dto);
    Task<List<CategoryDTO>> GetAllAsync();
}
