namespace Domain.Interfaces.Repositories;
public interface IRepositoryWrapper
{
    public ICategoryRepository CategoryRepository { get; }
    public ISubCategoryRepository SubCategoryRepository { get; }
}
