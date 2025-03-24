namespace Domain.Interfaces.Services
{
    public interface IServiceWrapper
    {
        public ICategoryService CategoryService { get; }
        public ISubCategoryService SubCategoryService { get; }
    }
}
