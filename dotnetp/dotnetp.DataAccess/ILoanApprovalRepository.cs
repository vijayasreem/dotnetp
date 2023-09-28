Acceptance, DisbursedLoanAmount = @DisbursedLoanAmount WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", loanApproval.Id);
                command.Parameters.AddWithValue("@Identification", loanApproval.Identification);
                command.Parameters.AddWithValue("@ProofOfIncome", loanApproval.ProofOfIncome);
                command.Parameters.AddWithValue("@CreditHistory", loanApproval.CreditHistory);
                command.Parameters.AddWithValue("@EmploymentDetails", loanApproval.EmploymentDetails);
                command.Parameters.AddWithValue("@CreditCheck", loanApproval.CreditCheck);
                command.Parameters.AddWithValue("@LoanAmount", loanApproval.LoanAmount);
                command.Parameters.AddWithValue("@InterestRate", loanApproval.InterestRate);
                command.Parameters.AddWithValue("@VehicleValueAssessment", loanApproval.VehicleValueAssessment);
                command.Parameters.AddWithValue("@LoanOfferAcceptance", loanApproval.LoanOfferAcceptance);
                command.Parameters.AddWithValue("@DisbursedLoanAmount", loanApproval.DisbursedLoanAmount);

                await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}

using Dotnetp.DTO;

namespace Dotnetp.Service
{
    public interface ILoanApprovalRepository
    {
        Task<LoanApprovalModel> GetLoanApprovalByIdAsync(int id);
        Task<int> CreateLoanApprovalAsync(LoanApprovalModel loanApproval);
        Task<int> UpdateLoanApprovalAsync(LoanApprovalModel loanApproval);
    }
}