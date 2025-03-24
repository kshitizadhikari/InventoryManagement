using Domain.Interfaces.Services;

namespace Application.Services;
public class ServiceWrapper(
    ICategoryService categoryService,
    ISubCategoryService subCategoryService,
    IProductService productService
    ) : IServiceWrapper
{
    public ICategoryService _categoryService = categoryService;
    public ISubCategoryService _subCategoryService = subCategoryService;
    public IProductService _productService = productService;

    public ICategoryService CategoryService => _categoryService;
    public ISubCategoryService SubCategoryService => _subCategoryService;
    public IProductService ProductService => _productService;
}

