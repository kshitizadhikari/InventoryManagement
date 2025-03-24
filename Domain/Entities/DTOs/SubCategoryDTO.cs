namespace Domain.Entities.DTOs;
public class SubCategoryDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CategoryId{ get; set; }
    public CategoryDTO Category{ get; set; }
}
