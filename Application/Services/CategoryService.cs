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
        return (res.MapToDTO(), null);
    }

    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        var res = await _repos.CategoryRepository.GetAllAsync();

        if (res == null)
        {
            return new List<CategoryDTO>();
        }

        return res.MapToDTO();
    }

    public Task<(CategoryDTO?, ErrorResponse?)> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<(CategoryDTO?, ErrorResponse?)> UpdateAsync(CategoryDTO dto)
    {
        if (dto == null)
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 400,
                Title = "Updation Error",
                Message = "Category data cannot be null."
            });
        }

        var entity = await _repos.CategoryRepository.GetByIdAsync(Guid.Parse(dto.Id));
        if (entity == null)
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Fetching Error",
                Message = "Entity doesn't exist."
            });
        }

        entity = dto.MapToEntity();
        await _repos.CategoryRepository.UpdateAsync(entity);
        return (entity.MapToDTO(), null);
    }

}
