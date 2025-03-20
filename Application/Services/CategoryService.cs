using Application.Mappers;
using Domain.Entities.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;
public class CategoryService(IRepositoryWrapper repos) : ICategoryService
{
    private readonly IRepositoryWrapper _repos = repos;

    public async Task<CategoryDTO> CreateAsync(CategoryDTO dto)
    {
        var entity = dto.MapToEntity();
        return ((await _repos.CategoryRepository.CreateAsync(entity)).MapToDTO());
    }

    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        return ((await _repos.CategoryRepository.GetAllAsync()).MapToDTO());
    }
}
