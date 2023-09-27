using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp.Repository
{
    public class VerificationRepository : IVerificationService
    {
        private readonly string _connectionString;

        public VerificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddAsync(VerificationModel verification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    "INSERT INTO Verification (VendorName, VendorBankAccountNumber, VendorRoutingNumber, IsPaymentApprovalRequired, PaymentAmount) " +
                    "VALUES (@VendorName, @VendorBankAccountNumber, @VendorRoutingNumber, @IsPaymentApprovalRequired, @PaymentAmount);" +
                    "SELECT SCOPE_IDENTITY();",
                    connection);
                command.Parameters.AddWithValue("@VendorName", verification.VendorName);
                command.Parameters.AddWithValue("@VendorBankAccountNumber", verification.VendorBankAccountNumber);
                command.Parameters.AddWithValue("@VendorRoutingNumber", verification.VendorRoutingNumber);
                command.Parameters.AddWithValue("@IsPaymentApprovalRequired", verification.IsPaymentApprovalRequired);
                command.Parameters.AddWithValue("@PaymentAmount", verification.PaymentAmount);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<VerificationModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    "SELECT Id, VendorName, VendorBankAccountNumber, VendorRoutingNumber, IsPaymentApprovalRequired, PaymentAmount " +
                    "FROM Verification WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new VerificationModel
                        {
                            Id = reader.GetInt32(0),
                            VendorName = reader.GetString(1),
                            VendorBankAccountNumber = reader.GetString(2),
                            VendorRoutingNumber = reader.GetString(3),
                            IsPaymentApprovalRequired = reader.GetBoolean(4),
                            PaymentAmount = reader.GetDecimal(5)
                        };
                    }
                }
            }

            return null;
        }

        public async Task<List<VerificationModel>> GetAllAsync()
        {
            var verificationList = new List<VerificationModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    "SELECT Id, VendorName, VendorBankAccountNumber, VendorRoutingNumber, IsPaymentApprovalRequired, PaymentAmount " +
                    "FROM Verification",
                    connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        verificationList.Add(new VerificationModel
                        {
                            Id = reader.GetInt32(0),
                            VendorName = reader.GetString(1),
                            VendorBankAccountNumber = reader.GetString(2),
                            VendorRoutingNumber = reader.GetString(3),
                            IsPaymentApprovalRequired = reader.GetBoolean(4),
                            PaymentAmount = reader.GetDecimal(5)
                        });
                    }
                }
            }

            return verificationList;
        }

        public async Task<bool> UpdateAsync(VerificationModel verification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    "UPDATE Verification SET VendorName = @VendorName, VendorBankAccountNumber = @VendorBankAccountNumber, " +
                    "VendorRoutingNumber = @VendorRoutingNumber, IsPaymentApprovalRequired = @IsPaymentApprovalRequired, " +
                    "PaymentAmount = @PaymentAmount WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", verification.Id);
                command.Parameters.AddWithValue("@VendorName", verification.VendorName);
                command.Parameters.AddWithValue("@VendorBankAccountNumber", verification.VendorBankAccountNumber);
                command.Parameters.AddWithValue("@VendorRoutingNumber", verification.VendorRoutingNumber);
                command.Parameters.AddWithValue("@IsPaymentApprovalRequired", verification.IsPaymentApprovalRequired);
                command.Parameters.AddWithValue("@PaymentAmount", verification.PaymentAmount);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(
                    "DELETE FROM Verification WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }

    public class ValidationRepository
    {
        private readonly string _connectionString;

        public ValidationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddAsync(ValidationModel validation)
        {
            // TODO: Implement AddAsync for ValidationModel
            throw new NotImplementedException();
        }

        public async Task<ValidationModel> GetByIdAsync(int id)
        {
            // TODO: Implement GetByIdAsync for ValidationModel
            throw new NotImplementedException();
        }

        public async Task<List<ValidationModel>> GetAllAsync()
        {
            // TODO: Implement GetAllAsync for ValidationModel
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ValidationModel validation)
        {
            // TODO: Implement UpdateAsync for ValidationModel
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Implement DeleteAsync for ValidationModel
            throw new NotImplementedException();
        }
    }

    public class DisbursementRepository
    {
        private readonly string _connectionString;

        public DisbursementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddAsync(DisbursementModel disbursement)
        {
            // TODO: Implement AddAsync for DisbursementModel
            throw new NotImplementedException();
        }

        public async Task<DisbursementModel> GetByIdAsync(int id)
        {
            // TODO: Implement GetByIdAsync for DisbursementModel
            throw new NotImplementedException();
        }

        public async Task<List<DisbursementModel>> GetAllAsync()
        {
            // TODO: Implement GetAllAsync for DisbursementModel
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(DisbursementModel disbursement)
        {
            // TODO: Implement UpdateAsync for DisbursementModel
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // TODO: Implement DeleteAsync for DisbursementModel
            throw new NotImplementedException();
        }
    }
}