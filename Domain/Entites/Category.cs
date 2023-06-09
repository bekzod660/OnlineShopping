using Domain.Common;

namespace Domain.Model
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; }
        // public ICollection<Brand> Brands { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
