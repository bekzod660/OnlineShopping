using Domain.Common;

namespace Domain.Model
{
    public class Product : BaseAuditableEntity
    {
        // public Brand Brand { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public string ProductSize { get; set; }
        public int Count { get; set; }
        public string? Image { get; set; }
    }
}
