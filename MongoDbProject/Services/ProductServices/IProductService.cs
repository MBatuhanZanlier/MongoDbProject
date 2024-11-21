using MongoDbProject.Dtos.ProductDtos;

namespace MongoDbProject.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync(); 
        Task<GetByIdProductDto> GetByIdProductAsync(string id); 
        Task DeleteAsync(string id);
        Task UpdateProductAsync(UpdateProductDto productDto); 
        Task CreateProductAsync(CreateProductDto productDto);
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        
    }
}
