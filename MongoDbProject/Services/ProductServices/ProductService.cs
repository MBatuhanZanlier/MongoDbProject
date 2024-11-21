using AutoMapper;
using MongoDB.Driver;
using MongoDbProject.Dtos.ProductDtos;
using MongoDbProject.Entities;
using MongoDbProject.Settings;

namespace MongoDbProject.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products; 
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseDateSettings _dateSettings)
        {
            var client = new MongoClient(_dateSettings.ConnectionString);
            var database = client.GetDatabase(_dateSettings.DatabaseName);
            _products = database.GetCollection<Product>(_dateSettings.ProductCollectionName);
           _categories = database.GetCollection<Category>(_dateSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var values = await _products.Find(x => true).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await _categories.Find(x => x.CategoryId == item.CategoryId).FirstOrDefaultAsync();
            }

            return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        }
        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var value = _mapper.Map<Product>(productDto);
            await _products.InsertOneAsync(value);
        }

        public async Task DeleteAsync(string id)
        {
            await _products.DeleteOneAsync(x => x.ProductId == id);

        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var value = _products.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(value);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = _products.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            return  _mapper.Map<GetByIdProductDto>(value);
        }

        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            var value=_mapper.Map<Product>(productDto); 
            await _products.FindOneAndReplaceAsync(x=>x.ProductId==productDto.ProductId, value);
        }
    }
}
