public interface ICreditCardService
{
    Task<IEnumerable<CreditCardModel>> GetAllAsync();
    Task<CreditCardModel> GetByIdAsync(int id);
    Task<int> CreateAsync(CreditCardModel creditCard);
    Task<bool> UpdateAsync(CreditCardModel creditCard);
    Task<bool> DeleteAsync(int id);
}