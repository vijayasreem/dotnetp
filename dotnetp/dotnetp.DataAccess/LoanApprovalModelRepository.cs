using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class LoanApprovalModelRepository : ILoanApprovalModelRepository
    {
        private readonly string _connectionString;

        public LoanApprovalModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(LoanApprovalModel loanApprovalModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO LoanApprovalModels (ValidIdentification, ProofOfIncome, CreditHistory, EmploymentDetails, CreditCheckPerformed, LoanAmount, InterestRateRange, VehicleAssessmentRequired, VehicleValue, LoanOfferAccepted, LoanDisbursed) " +
                               "VALUES (@ValidIdentification, @ProofOfIncome, @CreditHistory, @EmploymentDetails, @CreditCheckPerformed, @LoanAmount, @InterestRateRange, @VehicleAssessmentRequired, @VehicleValue, @LoanOfferAccepted, @LoanDisbursed);" +
                               "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ValidIdentification", loanApprovalModel.ValidIdentification);
                    command.Parameters.AddWithValue("@ProofOfIncome", loanApprovalModel.ProofOfIncome);
                    command.Parameters.AddWithValue("@CreditHistory", loanApprovalModel.CreditHistory);
                    command.Parameters.AddWithValue("@EmploymentDetails", loanApprovalModel.EmploymentDetails);
                    command.Parameters.AddWithValue("@CreditCheckPerformed", loanApprovalModel.CreditCheckPerformed);
                    command.Parameters.AddWithValue("@LoanAmount", loanApprovalModel.LoanAmount);
                    command.Parameters.AddWithValue("@InterestRateRange", loanApprovalModel.InterestRateRange);
                    command.Parameters.AddWithValue("@VehicleAssessmentRequired", loanApprovalModel.VehicleAssessmentRequired);
                    command.Parameters.AddWithValue("@VehicleValue", loanApprovalModel.VehicleValue);
                    command.Parameters.AddWithValue("@LoanOfferAccepted", loanApprovalModel.LoanOfferAccepted);
                    command.Parameters.AddWithValue("@LoanDisbursed", loanApprovalModel.LoanDisbursed);

                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task<LoanApprovalModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM LoanApprovalModels WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapLoanApprovalModelFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<LoanApprovalModel>> GetAllAsync()
        {
            List<LoanApprovalModel> loanApprovalModels = new List<LoanApprovalModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM LoanApprovalModels";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            LoanApprovalModel loanApprovalModel = MapLoanApprovalModelFromReader(reader);
                            loanApprovalModels.Add(loanApprovalModel);
                        }
                    }
                }
            }

            return loanApprovalModels;
        }

        public async Task UpdateAsync(LoanApprovalModel loanApprovalModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE LoanApprovalModels SET ValidIdentification = @ValidIdentification, ProofOfIncome = @ProofOfIncome, CreditHistory = @CreditHistory, " +
                               "EmploymentDetails = @EmploymentDetails, CreditCheckPerformed = @CreditCheckPerformed, LoanAmount = @LoanAmount, InterestRateRange = @InterestRateRange, " +
                               "VehicleAssessmentRequired = @VehicleAssessmentRequired, VehicleValue = @VehicleValue, LoanOfferAccepted = @LoanOfferAccepted, LoanDisbursed = @LoanDisbursed " +
                               "WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ValidIdentification", loanApprovalModel.ValidIdentification);
                    command.Parameters.AddWithValue("@ProofOfIncome", loanApprovalModel.ProofOfIncome);
                    command.Parameters.AddWithValue("@CreditHistory", loanApprovalModel.CreditHistory);
                    command.Parameters.AddWithValue("@EmploymentDetails", loanApprovalModel.EmploymentDetails);
                    command.Parameters.AddWithValue("@CreditCheckPerformed", loanApprovalModel.CreditCheckPerformed);
                    command.Parameters.AddWithValue("@LoanAmount", loanApprovalModel.LoanAmount);
                    command.Parameters.AddWithValue("@InterestRateRange", loanApprovalModel.InterestRateRange);
                    command.Parameters.AddWithValue("@VehicleAssessmentRequired", loanApprovalModel.VehicleAssessmentRequired);
                    command.Parameters.AddWithValue("@VehicleValue", loanApprovalModel.VehicleValue);
                    command.Parameters.AddWithValue("@LoanOfferAccepted", loanApprovalModel.LoanOfferAccepted);
                    command.Parameters.AddWithValue("@LoanDisbursed", loanApprovalModel.LoanDisbursed);
                    command.Parameters.AddWithValue("@Id", loanApprovalModel.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM LoanApprovalModels WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private LoanApprovalModel MapLoanApprovalModelFromReader(SqlDataReader reader)
        {
            return new LoanApprovalModel
            {
                Id = (int)reader["Id"],
                ValidIdentification = reader["ValidIdentification"].ToString(),
                ProofOfIncome = reader["ProofOfIncome"].ToString(),
                CreditHistory = reader["CreditHistory"].ToString(),
                EmploymentDetails = reader["EmploymentDetails"].ToString(),
                CreditCheckPerformed = (bool)reader["CreditCheckPerformed"],
                LoanAmount = (decimal)reader["LoanAmount"],
                InterestRateRange = (decimal)reader["InterestRateRange"],
                VehicleAssessmentRequired = (bool)reader["VehicleAssessmentRequired"],
                VehicleValue = (decimal)reader["VehicleValue"],
                LoanOfferAccepted = (bool)reader["LoanOfferAccepted"],
                LoanDisbursed = (bool)reader["LoanDisbursed"]
            };
        }
    }
}