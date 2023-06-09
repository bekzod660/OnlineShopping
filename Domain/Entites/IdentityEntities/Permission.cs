namespace Domain.Entites.IdentityEntities
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Role>? Roles { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
