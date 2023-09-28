using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DataAccess;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public class LoanApprovalModelService : ILoanApprovalModelService
    {
        private readonly ILoanApprovalModelRepository _repository;

        public LoanApprovalModelService(ILoanApprovalModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(LoanApprovalModel loanApprovalModel)
        {
            return await _repository.CreateAsync(loanApprovalModel);
        }

        public async Task<LoanApprovalModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<LoanApprovalModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(LoanApprovalModel loanApprovalModel)
        {
            await _repository.UpdateAsync(loanApprovalModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}