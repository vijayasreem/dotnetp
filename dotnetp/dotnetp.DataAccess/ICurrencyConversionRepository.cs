


using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface ICurrencyConversionRepository
    {
        Task<int> CreateAsync(CurrencyConversionModel model);
        Task<CurrencyConversionModel> GetByIdAsync(int id);
        Task<List<CurrencyConversionModel>> GetAllAsync();
        Task UpdateAsync(CurrencyConversionModel model);
        Task DeleteAsync(int id);
    }
}
