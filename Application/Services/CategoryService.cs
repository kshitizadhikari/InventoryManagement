using Application.Mappers;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;
public class CategoryService(IRepositoryWrapper repos) : ICategoryService
{
    private readonly IRepositoryWrapper _repos = repos;

    public async Task<(CategoryDTO?, ErrorResponse?)> CreateAsync(CategoryDTO dto)
    {
        if (dto == null)
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 400,
                Title = "Creation Error",
                Message = "Category data cannot be null"
            });
        }
        var entity = dto.MapToEntity();

        var res = await _repos.CategoryRepository.CreateAsync(entity);
        var resDto = res.MapToDTO();

        return (resDto, null);
    }

    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        return (await _repos.CategoryRepository.GetAllAsync()).MapToDTO();
    }
}
