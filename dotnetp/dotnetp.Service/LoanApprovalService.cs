public class LoanApprovalService : ILoanApprovalRepository
{
    private readonly LoanApprovalDataAccess _dataAccess;

    public LoanApprovalService(LoanApprovalDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public Task<LoanApprovalModel> GetLoanApprovalByIdAsync(int id)
    {
        return _dataAccess.GetLoanApprovalByIdAsync(id);
    }

    public Task<int> CreateLoanApprovalAsync(LoanApprovalModel loanApproval)
    {
        return _dataAccess.CreateLoanApprovalAsync(loanApproval);
    }

    public Task<int> UpdateLoanApprovalAsync(LoanApprovalModel loanApproval)
    {
        return _dataAccess.UpdateLoanApprovalAsync(loanApproval);
    }
}