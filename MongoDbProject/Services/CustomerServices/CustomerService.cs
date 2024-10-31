using AutoMapper;
using MongoDB.Driver;
using MongoDbProject.Dtos.CustomerDtos;
using MongoDbProject.Entities;
using MongoDbProject.Settings;

namespace MongoDbProject.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customerCollection;
        private readonly IMapper _mapper;
        public CustomerService(IMapper mapper, IDatabaseDateSettings _databaseDateSettings)
        {
            var client=new MongoClient(_databaseDateSettings.ConnectionString); 
            var database=client.GetDatabase(_databaseDateSettings.DatabaseName); 
            _customerCollection=database.GetCollection<Customer>(_databaseDateSettings.CustomerCollectionName);  
            _mapper=mapper;
        }
        public async Task CreateCustomerAsync(CreateCustomerDto customerDto)
        {
           var value=_mapper.Map<Customer>(customerDto); 
            await _customerCollection.InsertOneAsync(value);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(x=>x.CustomerId == id);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            var values = await _customerCollection.Find(x => true).ToListAsync(); 
            return _mapper.Map<List<ResultCustomerDto>>(values);
        }

        public async Task<GetByIdCustomerDto> GetByIdCustomerAsync(string id)
        {
            var values=await _customerCollection.Find<Customer>(x => x.CustomerId == id).FirstOrDefaultAsync(); 
            return _mapper.Map<GetByIdCustomerDto>(values);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto customerDto)
        {
            var value = _mapper.Map<Customer>(customerDto); 
            await _customerCollection.FindOneAndReplaceAsync(x=>x.CustomerId ==customerDto.CustomerId, value);
        }
    }
}
