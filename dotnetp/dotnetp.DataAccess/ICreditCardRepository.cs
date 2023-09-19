


using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface ICreditCardRepository
    {
        Task<IEnumerable<CreditCardModel>> GetAllAsync();
        Task<CreditCardModel> GetByIdAsync(int id);
        Task<int> CreateAsync(CreditCardModel creditCard);
        Task<bool> UpdateAsync(CreditCardModel creditCard);
        Task<bool> DeleteAsync(int id);
    }
}
