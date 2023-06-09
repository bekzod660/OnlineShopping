using Domain.Models;

namespace Domain.Entites.IdentityEntities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Permission>? Permissions { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
