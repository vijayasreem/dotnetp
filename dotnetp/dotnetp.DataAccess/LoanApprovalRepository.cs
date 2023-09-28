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

        public async Task<int> DeleteLoanApprovalAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM LoanApproval WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();

                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}

using Dotnetp.DTO;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dotnetp
{
    public class LoanApprovalRepository : ILoanApprovalRepository
    {
        private readonly string _connectionString;

        public LoanApprovalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<LoanApprovalModel> GetLoanApprovalByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM LoanApproval WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new LoanApprovalModel
                        {
                            Id = (int)reader["Id"],
                            Identification = (string)reader["Identification"],
                            ProofOfIncome = (string)reader["ProofOfIncome"],
                            CreditHistory = (string)reader["CreditHistory"],
                            EmploymentDetails = (string)reader["EmploymentDetails"],
                            CreditCheck = (string)reader["CreditCheck"],
                            LoanAmount = (int)reader["LoanAmount"],
                            InterestRate = (float)reader["InterestRate"],
                            VehicleValueAssessment = (string)reader["VehicleValueAssessment"],
                            LoanOfferAcceptance = (string)reader["LoanOfferAcceptance"],
                            DisbursedLoanAmount = (int)reader["DisbursedLoanAmount"]
                        };
                    }

                    return null;
                }
            }
        }

        public async Task<int> CreateLoanApprovalAsync(LoanApprovalModel loanApproval)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO LoanApproval (Identification, ProofOfIncome, CreditHistory, EmploymentDetails, CreditCheck, LoanAmount, InterestRate, VehicleValueAssessment, LoanOfferAcceptance, DisbursedLoanAmount) VALUES (@Identification, @ProofOfIncome, @CreditHistory, @EmploymentDetails, @CreditCheck, @LoanAmount, @InterestRate, @