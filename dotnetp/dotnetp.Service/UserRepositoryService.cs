using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<UserModel> Create(UserModel user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<bool> Update(UserModel user)
        {
            return await _userRepository.Update(user);
        }

        public async Task<bool> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<bool> VerifyDocuments(string documentPath)
        {
            // Logic to verify document format (PDF or JPEG) and throw exception if invalid
            return true;
        }

        public async Task<bool> ValidateCreditEvaluation(decimal income)
        {
            // Logic to validate credit evaluation based on income
            return income > 100000;
        }

        public async Task<bool> CheckCustomerAge(int age)
        {
            // Logic to check customer age
            return age >= 18 && age <= 65;
        }

        public async Task<bool> CheckCreditScore(int creditScore)
        {
            // Logic to check credit score
            return creditScore > 600;
        }

        public async Task<bool> ProcessDisbursement(string vendorName, decimal paymentAmount, string bankAccountNumber, string routingNumber, decimal availableBalance)
        {
            // Logic to process disbursement
            if (availableBalance >= paymentAmount)
            {
                if (paymentAmount <= 1000.0)
                {
                    // Automatically approve payments equal to or less than $1000.0
                    return true;
                }
                else
                {
                    // Prompt for payment approval if payment approval is required and not granted
                    return await PromptPaymentApproval();
                }
            }
            else
            {
                // Display a message indicating the lack of funds
                return false;
            }
        }

        private async Task<bool> PromptPaymentApproval()
        {
            // Logic to prompt for payment approval
            return true;
        }

        public async Task<bool> VerifyVendorInformation(string vendorName, string bankAccountNumber, string routingNumber)
        {
            // Logic to verify vendor information
            return bankAccountNumber.Length == 9 && routingNumber.Length == 9;
        }
    }
}