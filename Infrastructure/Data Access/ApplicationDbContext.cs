using Application.Abstraction;
using Application.Models.Token;
using Domain.Entites.IdentityEntities;
using Domain.Model;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data_Access
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // public DbSet<Brand> Brands { get; set; }
        // public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
    }
}
