using Application.Abstraction;
using Application.Interfaces;
using Domain.Model;
using Infrastructure.Repository.Generic_Repository;

namespace Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(Application.Abstraction.IApplicationDbContext catalogDb) : base(catalogDb)
        {
        }
    }
}
