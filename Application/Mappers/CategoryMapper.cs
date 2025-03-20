using Domain.Entities;
using Domain.Entities.DTOs;

namespace Application.Mappers;
public static class CategoryMapper
{
    public static Category MapToEntity(this CategoryDTO dto)
    {
        return new Category
        {
            Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid() : Guid.Parse(dto.Id),
            Name = dto.Name
        };
    }

    public static List<Category> MapToEntity(this IList<CategoryDTO> dtos)
    {
        return dtos.Select(x => x.MapToEntity()).ToList();
    }

    public static CategoryDTO MapToDTO(this Category entity)
    {
        return new CategoryDTO
        {
            Id = entity.Id.ToString(),
            Name = entity.Name
        };
    }

    public static List<CategoryDTO> MapToDTO(this IList<Category> entities)
    {
        return entities.Select(x => x.MapToDTO()).ToList();
    }
}
