﻿using Domain.Entities;
using Domain.Entities.DTOs;

namespace Domain.Interfaces.Services;
public interface ICategoryService
{
    Task<(CategoryDTO?, ErrorResponse?)> CreateAsync(CategoryDTO dto);
    Task<List<CategoryDTO>> GetAllAsync();
}
