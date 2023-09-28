public interface ILoanApprovalRepository
{
    Task<LoanApprovalModel> GetLoanApprovalByIdAsync(int id);
    Task<int> CreateLoanApprovalAsync(LoanApprovalModel loanApproval);
    Task<int> UpdateLoanApprovalAsync(LoanApprovalModel loanApproval);
}