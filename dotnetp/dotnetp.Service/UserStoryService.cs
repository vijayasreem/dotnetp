using dotnetp.DataAccess;
using dotnetp.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp.Service
{
    public class UserStoryService : IUserStoryService
    {
        private readonly IUserStoryRepository _repository;

        public UserStoryService(IUserStoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(UserStoryModel model)
        {
            // Verify documents
            if (!IsValidFileFormat(model.Document))
            {
                throw new InvalidFileException("Invalid file format. Only PDF or JPEG files are allowed.");
            }

            // Validate credit evaluation
            if (!ValidateCreditEvaluation(model.CreditEvaluation))
            {
                throw new InvalidCreditEvaluationException("Invalid credit evaluation.");
            }

            // Check customer age
            if (!CheckCustomerAge(model.CustomerAge))
            {
                throw new InvalidCustomerAgeException("Invalid customer age.");
            }

            // Check credit score
            if (!CheckCreditScore(model.CreditScore))
            {
                throw new InvalidCreditScoreException("Invalid credit score.");
            }

            // Process disbursement
            var vendor = await _repository.GetVendorAsync(model.VendorId);

            if (vendor == null)
            {
                throw new VendorNotFoundException("Vendor not found.");
            }

            if (!CheckBankAccountNumber(vendor.BankAccountNumber))
            {
                throw new InvalidBankAccountNumberException("Invalid bank account number.");
            }

            if (!CheckRoutingNumber(vendor.RoutingNumber))
            {
                throw new InvalidRoutingNumberException("Invalid routing number.");
            }

            if (!CheckAvailableBalance(vendor.AvailableBalance, model.PaymentAmount))
            {
                throw new InsufficientFundsException("Insufficient funds.");
            }

            if (model.PaymentAmount <= 1000.0)
            {
                return await _repository.CreateAsync(model);
            }
            else
            {
                if (model.PaymentApprovalRequired)
                {
                    if (!model.PaymentApproved)
                    {
                        throw new PaymentApprovalRequiredException("Payment approval required.");
                    }
                }

                var disbursementResult = await ProcessDisbursement(vendor.Name, model.PaymentAmount);
                return await _repository.CreateAsync(model);
            }
        }

        public async Task<UserStoryModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<UserStoryModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(UserStoryModel model)
        {
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private bool IsValidFileFormat(string document)
        {
            // Check file format
            var fileExtension = document.Substring(document.LastIndexOf('.') + 1).ToLower();
            return fileExtension == "pdf" || fileExtension == "jpeg";
        }

        private bool ValidateCreditEvaluation(CreditEvaluation evaluation)
        {
            // Check income for salaried employees
            if (evaluation.EmployeeType == EmployeeType.Salaried && evaluation.Income < 100000)
            {
                return false;
            }

            return true;
        }

        private bool CheckCustomerAge(int age)
        {
            // Check customer age
            return age >= 18 && age <= 65;
        }

        private bool CheckCreditScore(int creditScore)
        {
            // Check credit score
            return creditScore > 600;
        }

        private bool CheckBankAccountNumber(string accountNumber)
        {
            // Check bank account number length
            return accountNumber.Length == 9;
        }

        private bool CheckRoutingNumber(string routingNumber)
        {
            // Check routing number length
            return routingNumber.Length == 9;
        }

        private bool CheckAvailableBalance(decimal availableBalance, decimal paymentAmount)
        {
            // Check available balance
            return availableBalance >= paymentAmount;
        }

        private async Task<string> ProcessDisbursement(string vendorName, decimal paymentAmount)
        {
            // Process disbursement
            // ...
            return $"Disbursement process successful for vendor {vendorName} with payment amount {paymentAmount}.";
        }
    }
}