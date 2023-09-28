
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Service
{
    public interface ILoanApprovalModelRepository
    {
        Task<int> CreateAsync(LoanApprovalModel loanApprovalModel);
        Task<LoanApprovalModel> GetByIdAsync(int id);
        Task<List<LoanApprovalModel>> GetAllAsync();
        Task UpdateAsync(LoanApprovalModel loanApprovalModel);
        Task DeleteAsync(int id);
    }
}
