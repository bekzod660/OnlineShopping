using Application.Abstraction;
using Application.Interfaces;
using Domain.Model;
using Infrastructure.Repository.Generic_Repository;

namespace Application.Services;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(Abstraction.IApplicationDbContext catalogDb) : base(catalogDb)
    {
    }
}
