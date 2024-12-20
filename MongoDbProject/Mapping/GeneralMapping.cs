﻿using AutoMapper;
using MongoDbProject.Dtos.CategoryDtos;
using MongoDbProject.Dtos.CustomerDtos;
using MongoDbProject.Dtos.ProductDtos;
using MongoDbProject.Entities;

namespace MongoDbProject.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,ResultCategoryDto>().ReverseMap(); 
            CreateMap<Category,CreateCategoryDto>().ReverseMap(); 
            CreateMap<Category,UpdateCategoryDto>().ReverseMap(); 
            CreateMap<Category,GetByIdCategoryDto>().ReverseMap(); 
             
            
            CreateMap<Product,ResultProductDto>().ReverseMap(); 
            CreateMap<Product,CreateProductDto>().ReverseMap(); 
            CreateMap<Product,UpdateProductDto>().ReverseMap(); 
            CreateMap<Product,GetByIdProductDto>().ReverseMap();
            CreateMap<Product,ResultProductsWithCategoryDto>().ReverseMap();

            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetByIdCustomerDto>().ReverseMap();

        }
    }
}
