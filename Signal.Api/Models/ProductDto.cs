

namespace Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public long Price { get; set; }
        public IFormFile Photo_url { get; set; }
        public Guid CategoryId { get; set; }
    }
}
