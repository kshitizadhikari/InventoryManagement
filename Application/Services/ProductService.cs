﻿using Application.Common.Mappers;
using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services;
public class ProductService(IRepositoryWrapper repos) : IProductService
{
    private readonly IRepositoryWrapper _repos = repos;

    public async Task<(ProductDTO?, ErrorResponse?)> CreateAsync(ProductDTO dto)
    {
        var entity = dto.MapToEntity();

        if (!await _repos.CategoryRepository.ExistsAsync(Guid.Parse(dto.CategoryId)))
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Fetching Error",
                Message = "Product doesn't belong to any category."
            });
        }
        entity.CategoryId = Guid.Parse(dto.CategoryId);

        if (dto.SubCategoryId == null || !await _repos.SubCategoryRepository.ExistsAsync(Guid.Parse(dto.SubCategoryId)))
        {
            entity.SubCategoryId = null;
        }
        else
        {
            entity.SubCategoryId = Guid.Parse(dto.SubCategoryId);
        }

        var res = await _repos.ProductRepository.CreateAsync(entity);
        return (res.MapToDTO(), null);
    }

    public async Task<List<ProductDTO>> GetAllAsync()
    {
        var res = await _repos.ProductRepository.GetAllAsync();
        return res.MapToDTO();
    }

    public async Task<(ProductDTO?, ErrorResponse?)> GetByIdAsync(string id)
    {

        var entity = await _repos.ProductRepository.GetByIdAsync(Guid.Parse(id));

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

    public async Task<(ProductDTO?, ErrorResponse?)> UpdateAsync(ProductDTO dto)
    {
        var entity = await _repos.ProductRepository.GetByIdAsync(Guid.Parse(dto.Id));
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
        entity.Description = dto.Description;
        entity.Price = dto.Price;

        if (!await _repos.CategoryRepository.ExistsAsync(Guid.Parse(dto.CategoryId)))
        {
            return (null, new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Fetching Error",
                Message = "Product doesn't belong to any category."
            });
        }
        entity.CategoryId = Guid.Parse(dto.CategoryId);

        if (dto.SubCategoryId == null || !await _repos.SubCategoryRepository.ExistsAsync(Guid.Parse(dto.SubCategoryId)))
        {
            entity.SubCategoryId = null;
        }
        else
        {
            entity.SubCategoryId = Guid.Parse(dto.SubCategoryId);
        }

        await _repos.ProductRepository.UpdateAsync(entity);
        return (entity.MapToDTO(), null);
    }

    public async Task<ErrorResponse?> DeleteAsync(string id)
    {

        if (!await _repos.ProductRepository.DeleteAsync(Guid.Parse(id)))
        {
            return new ErrorResponse
            {
                ErrorCode = 500,
                Title = "Deletion Error",
                Message = "Failed to delete product."
            };
        }
        return null;
    }

}
