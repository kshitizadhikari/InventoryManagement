using Domain.Interfaces.Services;

namespace Application.Services;
public class ServiceWrapper(ICategoryService categoryService) : IServiceWrapper
{
    public ICategoryService _categoryService = categoryService;

    public ICategoryService CategoryService => _categoryService;
}

