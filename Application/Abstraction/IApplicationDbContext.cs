using Application.Models.Token;
using Domain.Entites.IdentityEntities;
using Domain.Model;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

    DbSet<T> Set<T>() where T : class;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
