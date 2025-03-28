﻿using Application.Common.Mappers;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;
public class SubCategoryService(IRepositoryWrapper repos) : ISubCategoryService
{
    private readonly IRepositoryWrapper _repos = repos;

    public async Task<(SubCategoryDTO?, ErrorResponse?)> CreateAsync(SubCategoryDTO dto)
    {
        var entity = dto.MapToEntity();
        var res = await _repos.SubCategoryRepository.CreateAsync(entity);
        return (res.MapToDTO(), null);
    }

    public async Task<List<SubCategoryDTO>> GetAllAsync()
    {
        var res = await _repos.SubCategoryRepository.GetAllAsync();
        return res.MapToDTO();
    }

    public async Task<(SubCategoryDTO?, ErrorResponse?)> GetByIdAsync(string id)
    {
        var entity = await _repos.SubCategoryRepository.GetByIdAsync(Guid.Parse(id));
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

    public async Task<(SubCategoryDTO?, ErrorResponse?)> UpdateAsync(SubCategoryDTO dto)
    {
        var entity = await _repos.SubCategoryRepository.GetByIdAsync(Guid.Parse(dto.Id));
        if (entity == null)
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Fetching Error",
                Message = "Entity doesn't exist."
            });
        }

        if (!await _repos.CategoryRepository.ExistsAsync(Guid.Parse(dto.CategoryId)))
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Fetching Error",
                Message = "Invalid Category Id."
            });
        }

        entity.CategoryId = Guid.Parse(dto.CategoryId);
        entity.Name = dto.Name;

        await _repos.SubCategoryRepository.UpdateAsync(entity);
        return (entity.MapToDTO(), null);
    }

    public async Task<ErrorResponse?> DeleteAsync(string id)
    {
        if (!await _repos.SubCategoryRepository.DeleteAsync(Guid.Parse(id)))
        {
            return new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Deletion Error",
                Message = "Failed to delete sub-category."
            };
        }
        return null;
    }

};
