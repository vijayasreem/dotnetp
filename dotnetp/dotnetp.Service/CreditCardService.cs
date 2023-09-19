using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardService(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public async Task<IEnumerable<CreditCardModel>> GetAllAsync()
        {
            return await _creditCardRepository.GetAllAsync();
        }

        public async Task<CreditCardModel> GetByIdAsync(int id)
        {
            return await _creditCardRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(CreditCardModel creditCard)
        {
            return await _creditCardRepository.CreateAsync(creditCard);
        }

        public async Task<bool> UpdateAsync(CreditCardModel creditCard)
        {
            return await _creditCardRepository.UpdateAsync(creditCard);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _creditCardRepository.DeleteAsync(id);
        }
    }
}