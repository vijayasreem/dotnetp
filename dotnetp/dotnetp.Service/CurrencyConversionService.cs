using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly ICurrencyConversionRepository _repository;

        public CurrencyConversionService(ICurrencyConversionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(CurrencyConversionModel model)
        {
            return await _repository.CreateAsync(model);
        }

        public async Task<CurrencyConversionModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<CurrencyConversionModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(CurrencyConversionModel model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}