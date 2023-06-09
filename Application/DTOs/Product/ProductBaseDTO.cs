namespace Application.DTOs.Product
{
    public class ProductBaseDTO
    {
        // public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public string ProductSize { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }
    }
}
