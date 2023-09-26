public interface ICurrencyConversionService
{
    Task<int> CreateAsync(CurrencyConversionModel model);
    Task<CurrencyConversionModel> GetByIdAsync(int id);
    Task<List<CurrencyConversionModel>> GetAllAsync();
    Task UpdateAsync(CurrencyConversionModel model);
    Task DeleteAsync(int id);
}