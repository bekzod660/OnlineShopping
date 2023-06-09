using Application.Abstraction;
using Application.Interfaces;
using Domain.Entites.IdentityEntities;
using Infrastructure.Repository.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Services
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public readonly IApplicationDbContext _db;
        public RoleRepository(Abstraction.IApplicationDbContext catalogDb) : base(catalogDb)
        {
            _db = catalogDb;
        }
        public override Task<Role> CreateAsync(Role entity)
        {
            var newRole = _db.Roles.Attach(entity);
            _db.SaveChangesAsync();
            return Task.FromResult(entity);
        }
        public override Task<IQueryable<Role>> GetAsync(Expression<Func<Role, bool>> expression)
        {
            return Task.FromResult(_db.Roles.Where(expression).Include(x => x.Permissions).AsQueryable());
        }
        public override Task<Role> GetByIdAsync(Guid id)
        {
            return Task.FromResult((_db?.Roles?.Where(x => x.Id == id).Include(x => x.Permissions).First()));
        }
    }
}
