using MongoDbProject.Dtos.CategoryDtos;
using MongoDbProject.Entities;

namespace MongoDbProject.Dtos.ProductDtos
{
    public class ResultProductsWithCategoryDto
    {
        public string? ProductId { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? SavedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
