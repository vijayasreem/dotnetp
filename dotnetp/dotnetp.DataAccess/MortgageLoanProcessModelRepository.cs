using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Repository
{
    public class MortgageLoanProcessModelRepository : IMortgageLoanProcessModelService
    {
        private readonly string _connectionString;

        public MortgageLoanProcessModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(MortgageLoanProcessModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO MortgageLoanProcessModel (CustomerInformation, VerifiedDocuments, CreditCheckPassed, PreQualified, LoanApproved, VehicleAssessment, LoanAccepted, LoanAmount, LoanDisbursed, Results) VALUES (@CustomerInformation, @VerifiedDocuments, @CreditCheckPassed, @PreQualified, @LoanApproved, @VehicleAssessment, @LoanAccepted, @LoanAmount, @LoanDisbursed, @Results); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@CustomerInformation", model.CustomerInformation);
                    command.Parameters.AddWithValue("@VerifiedDocuments", model.VerifiedDocuments);
                    command.Parameters.AddWithValue("@CreditCheckPassed", model.CreditCheckPassed);
                    command.Parameters.AddWithValue("@PreQualified", model.PreQualified);
                    command.Parameters.AddWithValue("@LoanApproved", model.LoanApproved);
                    command.Parameters.AddWithValue("@VehicleAssessment", model.VehicleAssessment);
                    command.Parameters.AddWithValue("@LoanAccepted", model.LoanAccepted);
                    command.Parameters.AddWithValue("@LoanAmount", model.LoanAmount);
                    command.Parameters.AddWithValue("@LoanDisbursed", model.LoanDisbursed);
                    command.Parameters.AddWithValue("@Results", model.Results);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<MortgageLoanProcessModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM MortgageLoanProcessModel WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapMortgageLoanProcessModel(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<MortgageLoanProcessModel>> GetAllAsync()
        {
            List<MortgageLoanProcessModel> models = new List<MortgageLoanProcessModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM MortgageLoanProcessModel", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            models.Add(MapMortgageLoanProcessModel(reader));
                        }
                    }
                }
            }

            return models;
        }

        public async Task<int> UpdateAsync(MortgageLoanProcessModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE MortgageLoanProcessModel SET CustomerInformation = @CustomerInformation, VerifiedDocuments = @VerifiedDocuments, CreditCheckPassed = @CreditCheckPassed, PreQualified = @PreQualified, LoanApproved = @LoanApproved, VehicleAssessment = @VehicleAssessment, LoanAccepted = @LoanAccepted, LoanAmount = @LoanAmount, LoanDisbursed = @LoanDisbursed, Results = @Results WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@CustomerInformation", model.CustomerInformation);
                    command.Parameters.AddWithValue("@VerifiedDocuments", model.VerifiedDocuments);
                    command.Parameters.AddWithValue("@CreditCheckPassed", model.CreditCheckPassed);
                    command.Parameters.AddWithValue("@PreQualified", model.PreQualified);
                    command.Parameters.AddWithValue("@LoanApproved", model.LoanApproved);
                    command.Parameters.AddWithValue("@VehicleAssessment", model.VehicleAssessment);
                    command.Parameters.AddWithValue("@LoanAccepted", model.LoanAccepted);
                    command.Parameters.AddWithValue("@LoanAmount", model.LoanAmount);
                    command.Parameters.AddWithValue("@LoanDisbursed", model.LoanDisbursed);
                    command.Parameters.AddWithValue("@Results", model.Results);
                    command.Parameters.AddWithValue("@Id", model.Id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM MortgageLoanProcessModel WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        private MortgageLoanProcessModel MapMortgageLoanProcessModel(SqlDataReader reader)
        {
            return new MortgageLoanProcessModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                CustomerInformation = reader["CustomerInformation"].ToString(),
                VerifiedDocuments = reader["VerifiedDocuments"].ToString(),
                CreditCheckPassed = Convert.ToBoolean(reader["CreditCheckPassed"]),
                PreQualified = Convert.ToBoolean(reader["PreQualified"]),
                LoanApproved = Convert.ToBoolean(reader["LoanApproved"]),
                VehicleAssessment = reader["VehicleAssessment"].ToString(),
                LoanAccepted = Convert.ToBoolean(reader["LoanAccepted"]),
                LoanAmount = Convert.ToDecimal(reader["LoanAmount"]),
                LoanDisbursed = Convert.ToBoolean(reader["LoanDisbursed"]),
                Results = reader["Results"].ToString()
            };
        }
    }
}