using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public interface ICreditCardRepository
    {
        Task<int> CreateAsync(CreditCardModel creditCard);
        Task<CreditCardModel> GetByIdAsync(int id);
        Task<List<CreditCardModel>> GetAllAsync();
        Task UpdateAsync(CreditCardModel creditCard);
        Task DeleteAsync(int id);
    }
}