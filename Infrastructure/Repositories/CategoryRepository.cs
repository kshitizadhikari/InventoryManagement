using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class CategoryRepository : BaseRepository<Category>
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
