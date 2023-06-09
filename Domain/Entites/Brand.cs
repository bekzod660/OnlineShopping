using Domain.Common;

namespace Domain.Model
{
    public class Brand : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
