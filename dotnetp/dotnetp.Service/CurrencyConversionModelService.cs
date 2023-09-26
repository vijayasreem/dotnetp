using dotnetp.DataAccess;
using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class CurrencyConversionModelService : ICurrencyConversionModelService
    {
        private readonly ICurrencyConversionModelRepository _repository;

        public CurrencyConversionModelService(ICurrencyConversionModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(CurrencyConversionModel model)
        {
            // Add your implementation here
            return await _repository.CreateAsync(model);
        }

        public async Task<CurrencyConversionModel> GetByIdAsync(int id)
        {
            // Add your implementation here
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CurrencyConversionModel>> GetAllAsync()
        {
            // Add your implementation here
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(CurrencyConversionModel model)
        {
            // Add your implementation here
            return await _repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Add your implementation here
            return await _repository.DeleteAsync(id);
        }
    }
}