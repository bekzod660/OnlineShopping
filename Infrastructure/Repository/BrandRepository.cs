using Application.Interfaces;
using Domain.Model;
using Infrastructure.Repository.Generic_Repository;

namespace Infrastructure.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(Application.Abstraction.IApplicationDbContext catalogDb) : base(catalogDb)
        {
        }

    }

}