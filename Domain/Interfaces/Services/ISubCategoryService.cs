using Domain.Entities;
using Domain.Entities.DTOs;

namespace Domain.Interfaces.Services;
public interface ISubCategoryService 
{
    Task<(SubCategoryDTO?, ErrorResponse?)> CreateAsync(SubCategoryDTO dto);
    Task<List<SubCategoryDTO>> GetAllAsync();
    Task<(SubCategoryDTO?, ErrorResponse?)> GetByIdAsync(string id);
    Task<(SubCategoryDTO?, ErrorResponse?)> UpdateAsync(SubCategoryDTO dto);
}
