using Application.Abstraction;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Repository.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Services;

internal class UserRepository : Repository<User>, IUserRepository
{
    private readonly IApplicationDbContext _db;
    public UserRepository(Abstraction.IApplicationDbContext catalogDb) : base(catalogDb)
    {
        _db = catalogDb;
    }
    public override Task<IQueryable<User>> GetAsync(Expression<Func<User, bool>> expression)
    {
        return Task.FromResult(_db.Users.Where(expression).Include(x => x.Roles).AsQueryable());
    }
    public override Task<User> GetByIdAsync(Guid id)
    {
        return Task.FromResult((_db.Users.Where(x => x.Id == id).Include(x => x.Roles).First()));
    }
}
