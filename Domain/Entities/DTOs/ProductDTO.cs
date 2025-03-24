namespace Domain.Entities.DTOs;
public class ProductDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryId { get; set; }
    public string SubCategoryId { get; set; }
    public CategoryDTO? Category { get; set; }
    public SubCategoryDTO? SubCategory { get; set; }
}
