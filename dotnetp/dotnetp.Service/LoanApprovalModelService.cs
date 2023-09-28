using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class LoanApprovalModelService : ILoanApprovalModelRepository
    {
        private readonly ILoanApprovalModelDataAccess _dataAccess;

        public LoanApprovalModelService(ILoanApprovalModelDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateAsync(LoanApprovalModel loanApprovalModel)
        {
            // Perform validation and business logic before creating the loan approval model
            // ...

            return await _dataAccess.CreateAsync(loanApprovalModel);
        }

        public async Task<LoanApprovalModel> GetByIdAsync(int id)
        {
            return await _dataAccess.GetByIdAsync(id);
        }

        public async Task<List<LoanApprovalModel>> GetAllAsync()
        {
            return await _dataAccess.GetAllAsync();
        }

        public async Task UpdateAsync(LoanApprovalModel loanApprovalModel)
        {
            // Perform validation and business logic before updating the loan approval model
            // ...

            await _dataAccess.UpdateAsync(loanApprovalModel);
        }

        public async Task DeleteAsync(int id)
        {
            // Perform validation and business logic before deleting the loan approval model
            // ...

            await _dataAccess.DeleteAsync(id);
        }
    }
}