using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;
public class RepositoryWrapper(ICategoryRepository categoryRepository) : IRepositoryWrapper
{
    public ICategoryRepository _categoryRepository = categoryRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;
}
