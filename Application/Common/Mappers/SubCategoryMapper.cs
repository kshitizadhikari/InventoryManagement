using Domain.Entities;
using Domain.Entities.DTOs;

namespace Application.Common.Mappers;
public static class SubSubCategoryMapper
{
    public static SubCategory MapToEntity(this SubCategoryDTO dto)
    {
        return new SubCategory
        {
            Id = Utils.ParseOrNewGuid(dto.Id),
            Name = dto.Name,
            CategoryId = Guid.Parse(dto.CategoryId)
        };
    }

    public static List<SubCategory> MapToEntity(this IList<SubCategoryDTO> dtos)
    {
        return dtos.Select(x => x.MapToEntity()).ToList();
    }

    public static SubCategoryDTO MapToDTO(this SubCategory entity)
    {
        return new SubCategoryDTO
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            CategoryId = entity.CategoryId.ToString(),
        };
    }

    public static List<SubCategoryDTO> MapToDTO(this IList<SubCategory> entities)
    {
        return entities.Select(x => x.MapToDTO()).ToList();
    }
}
