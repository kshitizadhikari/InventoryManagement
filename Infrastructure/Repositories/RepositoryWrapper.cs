using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;
public class RepositoryWrapper(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository) : IRepositoryWrapper
{
    public ICategoryRepository _categoryRepository = categoryRepository;
    public ISubCategoryRepository _subCategoryRepository = subCategoryRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;
    public ISubCategoryRepository SubCategoryRepository => _subCategoryRepository;
}
