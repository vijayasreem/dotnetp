using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public interface ICurrencyConversionModelService
    {
        Task<int> CreateAsync(CurrencyConversionModel model);
        Task<CurrencyConversionModel> GetByIdAsync(int id);
        Task<IEnumerable<CurrencyConversionModel>> GetAllAsync();
        Task<bool> UpdateAsync(CurrencyConversionModel model);
        Task<bool> DeleteAsync(int id);
    }
}