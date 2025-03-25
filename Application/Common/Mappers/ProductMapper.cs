using Domain.Entities;
using Domain.Entities.DTOs;

namespace Application.Common.Mappers;
public static class ProductMapper
{
    public static Product MapToEntity(this ProductDTO dto)
    {
        return new Product
        {
            Id = Utils.ParseOrNewGuid(dto.Id),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = Guid.Parse(dto.CategoryId),
            SubCategoryId = string.IsNullOrEmpty(dto.SubCategoryId) ? null : Guid.Parse(dto.SubCategoryId)
        };
    }

    public static List<Product> MapToEntity(this IList<ProductDTO> dtos)
    {
        return dtos?.Select(x => x.MapToEntity()).ToList() ?? [];
    }

    public static ProductDTO MapToDTO(this Product entity)
    {
        return new ProductDTO
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            CategoryId = entity.CategoryId.ToString(),
            SubCategoryId = entity.SubCategoryId?.ToString()
        };
    }

    public static List<ProductDTO> MapToDTO(this IList<Product> entities)
    {
        return entities?.Select(x => x.MapToDTO()).ToList() ?? [];
    }
}
