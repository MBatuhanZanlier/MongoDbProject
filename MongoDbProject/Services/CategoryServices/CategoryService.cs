using AutoMapper;
using MongoDB.Driver;
using MongoDbProject.Dtos.CategoryDtos;
using MongoDbProject.Entities;
using MongoDbProject.Settings;

namespace MongoDbProject.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, IDatabaseDateSettings _databaseDateSettings)
        {
            var client = new MongoClient(_databaseDateSettings.ConnectionString);
            var database = client.GetDatabase(_databaseDateSettings.DatabaseName);
            _categories = database.GetCollection<Category>(_databaseDateSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCategoyAsync(CreateCategoryDto categoryDto)
        {
            var value = _mapper.Map<Category>(categoryDto);
            await _categories.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categories.DeleteOneAsync(x => x.CategoryId == id);

        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = await _categories.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var value = await _categories.Find<Category>(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDto>(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            var value = _mapper.Map<Category>(categoryDto);
            await _categories.FindOneAndReplaceAsync(x => x.CategoryId == categoryDto.CategoryId, value);
        }
    }
}
