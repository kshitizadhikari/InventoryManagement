using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories;
public class RepositoryWrapper(
    ICategoryRepository categoryRepository,
    ISubCategoryRepository subCategoryRepository,
    IProductRepository productRepository
    ) : IRepositoryWrapper
{
    public ICategoryRepository _categoryRepository = categoryRepository;
    public ISubCategoryRepository _subCategoryRepository = subCategoryRepository;
    public IProductRepository _productRepository = productRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;
    public ISubCategoryRepository SubCategoryRepository => _subCategoryRepository;
    public IProductRepository ProductRepository => _productRepository;
}
