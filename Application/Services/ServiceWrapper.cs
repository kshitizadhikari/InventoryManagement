using Domain.Interfaces.Services;

namespace Application.Services;
public class ServiceWrapper(ICategoryService categoryService, ISubCategoryService subCategoryService) : IServiceWrapper
{
    public ICategoryService _categoryService = categoryService;
    public ISubCategoryService _subCategoryService = subCategoryService;

    public ICategoryService CategoryService => _categoryService;
    public ISubCategoryService ISubCategoryService => _subCategoryService;
}

