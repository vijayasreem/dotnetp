using System;
using System.Collections.Generic;
using System.Data;
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
                string query = @"INSERT INTO LoanApprovalModels (ValidIdentification, ProofOfIncome, CreditHistory, EmploymentDetails, LoanAmount, InterestRate, VehicleValue, LoanOfferAccepted, DisbursementDate)
                                VALUES (@ValidIdentification, @ProofOfIncome, @CreditHistory, @EmploymentDetails, @LoanAmount, @InterestRate, @VehicleValue, @LoanOfferAccepted, @DisbursementDate);
                                SELECT CAST(SCOPE_IDENTITY() AS INT);";
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ValidIdentification", loanApprovalModel.ValidIdentification);
                command.Parameters.AddWithValue("@ProofOfIncome", loanApprovalModel.ProofOfIncome);
                command.Parameters.AddWithValue("@CreditHistory", loanApprovalModel.CreditHistory);
                command.Parameters.AddWithValue("@EmploymentDetails", loanApprovalModel.EmploymentDetails);
                command.Parameters.AddWithValue("@LoanAmount", loanApprovalModel.LoanAmount);
                command.Parameters.AddWithValue("@InterestRate", loanApprovalModel.InterestRate);
                command.Parameters.AddWithValue("@VehicleValue", loanApprovalModel.VehicleValue);
                command.Parameters.AddWithValue("@LoanOfferAccepted", loanApprovalModel.LoanOfferAccepted);
                command.Parameters.AddWithValue("@DisbursementDate", loanApprovalModel.DisbursementDate);

                await connection.OpenAsync();
                int id = (int)await command.ExecuteScalarAsync();
                return id;
            }
        }

        public async Task<LoanApprovalModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM LoanApprovalModels WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    LoanApprovalModel loanApprovalModel = MapReaderToLoanApprovalModel(reader);
                    return loanApprovalModel;
                }

                return null;
            }
        }

        public async Task<List<LoanApprovalModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM LoanApprovalModels;";
                SqlCommand command = new SqlCommand(query, connection);

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<LoanApprovalModel> loanApprovalModels = new List<LoanApprovalModel>();

                while (await reader.ReadAsync())
                {
                    LoanApprovalModel loanApprovalModel = MapReaderToLoanApprovalModel(reader);
                    loanApprovalModels.Add(loanApprovalModel);
                }

                return loanApprovalModels;
            }
        }

        public async Task UpdateAsync(LoanApprovalModel loanApprovalModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE LoanApprovalModels SET 
                                ValidIdentification = @ValidIdentification,
                                ProofOfIncome = @ProofOfIncome,
                                CreditHistory = @CreditHistory,
                                EmploymentDetails = @EmploymentDetails,
                                LoanAmount = @LoanAmount,
                                InterestRate = @InterestRate,
                                VehicleValue = @VehicleValue,
                                LoanOfferAccepted = @LoanOfferAccepted,
                                DisbursementDate = @DisbursementDate
                                WHERE Id = @Id;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ValidIdentification", loanApprovalModel.ValidIdentification);
                command.Parameters.AddWithValue("@ProofOfIncome", loanApprovalModel.ProofOfIncome);
                command.Parameters.AddWithValue("@CreditHistory", loanApprovalModel.CreditHistory);
                command.Parameters.AddWithValue("@EmploymentDetails", loanApprovalModel.EmploymentDetails);
                command.Parameters.AddWithValue("@LoanAmount", loanApprovalModel.LoanAmount);
                command.Parameters.AddWithValue("@InterestRate", loanApprovalModel.InterestRate);
                command.Parameters.AddWithValue("@VehicleValue", loanApprovalModel.VehicleValue);
                command.Parameters.AddWithValue("@LoanOfferAccepted", loanApprovalModel.LoanOfferAccepted);
                command.Parameters.AddWithValue("@DisbursementDate", loanApprovalModel.DisbursementDate);
                command.Parameters.AddWithValue("@Id", loanApprovalModel.Id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM LoanApprovalModels WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        private LoanApprovalModel MapReaderToLoanApprovalModel(SqlDataReader reader)
        {
            LoanApprovalModel loanApprovalModel = new LoanApprovalModel();
            loanApprovalModel.Id = (int)reader["Id"];
            loanApprovalModel.ValidIdentification = (string)reader["ValidIdentification"];
            loanApprovalModel.ProofOfIncome = (string)reader["ProofOfIncome"];
            loanApprovalModel.CreditHistory = (string)reader["CreditHistory"];
            loanApprovalModel.EmploymentDetails = (string)reader["EmploymentDetails"];
            loanApprovalModel.LoanAmount = (decimal)reader["LoanAmount"];
            loanApprovalModel.InterestRate = (decimal)reader["InterestRate"];
            loanApprovalModel.VehicleValue = (string)reader["VehicleValue"];
            loanApprovalModel.LoanOfferAccepted = (bool)reader["LoanOfferAccepted"];
            loanApprovalModel.DisbursementDate = (DateTime)reader["DisbursementDate"];

            return loanApprovalModel;
        }
    }
}