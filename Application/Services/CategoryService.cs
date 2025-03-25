using Application.Common.Mappers;
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
        var entity = dto.MapToEntity();
        var res = await _repos.CategoryRepository.CreateAsync(entity);
        return (res.MapToDTO(), null);
    }

    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        var res = await _repos.CategoryRepository.GetAllAsync();
        return res.MapToDTO();
    }

    public async Task<(CategoryDTO?, ErrorResponse?)> GetByIdAsync(string id)
    {
        var entity = await _repos.CategoryRepository.GetByIdAsync(Guid.Parse(id));
        if (entity == null)
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Fetching Error",
                Message = "Entity doesn't exist."
            });
        }
        return (entity.MapToDTO(), null);

    }

    public async Task<(CategoryDTO?, ErrorResponse?)> UpdateAsync(CategoryDTO dto)
    {
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

        entity.Name = dto.Name;
        await _repos.CategoryRepository.UpdateAsync(entity);
        return (entity.MapToDTO(), null);
    }

    public async Task<ErrorResponse?> DeleteAsync(string id)
    {
        if (!await _repos.CategoryRepository.DeleteAsync(Guid.Parse(id)))
        {
            return new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Deletion Error",
                Message = "Failed to delete category."
            };
        }
        return null;
    }

}
