﻿using MongoDbProject.Dtos.CategoryDtos;

namespace MongoDbProject.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoyAsync(CreateCategoryDto categoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto categoryDto);
        Task DeleteCategoryAsync(string id); 
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);   
    }
}
