using Domain.Entities;
using Domain.Entities.DTOs;

namespace Domain.Interfaces.Services;
public interface IProductService
{
    Task<(ProductDTO?, ErrorResponse?)> CreateAsync(ProductDTO dto);
    Task<List<ProductDTO>> GetAllAsync();
    Task<(ProductDTO?, ErrorResponse?)> GetByIdAsync(string id);
    Task<(ProductDTO?, ErrorResponse?)> UpdateAsync(ProductDTO dto);
    Task<ErrorResponse?> DeleteAsync(string id);
}
