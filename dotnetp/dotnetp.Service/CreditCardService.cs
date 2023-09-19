using dotnetp.DataAccess;
using dotnetp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class CreditCardService : ICreditCardRepository
    {
        private readonly IDataAccess _dataAccess;

        public CreditCardService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateAsync(CreditCardModel creditCard)
        {
            // Encrypt customer data before storing it
            creditCard.Encrypt();

            // Call the data access layer to create the credit card
            return await _dataAccess.CreateAsync(creditCard);
        }

        public async Task<CreditCardModel> GetByIdAsync(int id)
        {
            // Call the data access layer to get the credit card by id
            var creditCard = await _dataAccess.GetByIdAsync(id);

            if (creditCard != null)
            {
                // Decrypt the customer data after retrieving it
                creditCard.Decrypt();
            }

            return creditCard;
        }

        public async Task<List<CreditCardModel>> GetAllAsync()
        {
            // Call the data access layer to get all credit cards
            var creditCards = await _dataAccess.GetAllAsync();

            foreach (var creditCard in creditCards)
            {
                // Decrypt the customer data after retrieving it
                creditCard.Decrypt();
            }

            return creditCards;
        }

        public async Task UpdateAsync(CreditCardModel creditCard)
        {
            // Encrypt customer data before updating it
            creditCard.Encrypt();

            // Call the data access layer to update the credit card
            await _dataAccess.UpdateAsync(creditCard);
        }

        public async Task DeleteAsync(int id)
        {
            // Call the data access layer to delete the credit card by id
            await _dataAccess.DeleteAsync(id);
        }
    }
}