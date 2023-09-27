using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface IUserRepositoryService
    {
        Task<UserModel> GetById(int id);
        Task<List<UserModel>> GetAll();
        Task<UserModel> Create(UserModel user);
        Task<bool> Update(UserModel user);
        Task<bool> Delete(int id);
        Task<bool> VerifyDocuments(string documentPath);
        Task<bool> ValidateCreditEvaluation(decimal income);
        Task<bool> CheckCustomerAge(int age);
        Task<bool> CheckCreditScore(int creditScore);
        Task<bool> ProcessDisbursement(string vendorName, decimal paymentAmount, string bankAccountNumber, string routingNumber, decimal availableBalance);
        Task<bool> VerifyVendorInformation(string vendorName, string bankAccountNumber, string routingNumber);
    }
}