using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public async Task<UserModel> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Users WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return MapUserModelFromReader(reader);
                }
                return null;
            }
        }
        
        public async Task<List<UserModel>> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Users";
                var command = new SqlCommand(query, connection);
                var reader = await command.ExecuteReaderAsync();
                var users = new List<UserModel>();
                while (await reader.ReadAsync())
                {
                    users.Add(MapUserModelFromReader(reader));
                }
                return users;
            }
        }
        
        public async Task<UserModel> Create(UserModel user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO Users (Name, Email, Password, PhoneNumber, Income, Age, CreditScore, BankAccountNumber, RoutingNumber, AvailableBalance, PaymentAmount, VendorName, PaymentApprovalRequired) " +
                            "VALUES (@Name, @Email, @Password, @PhoneNumber, @Income, @Age, @CreditScore, @BankAccountNumber, @RoutingNumber, @AvailableBalance, @PaymentAmount, @VendorName, @PaymentApprovalRequired);" +
                            "SELECT SCOPE_IDENTITY();";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@Income", user.Income);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@CreditScore", user.CreditScore);
                command.Parameters.AddWithValue("@BankAccountNumber", user.BankAccountNumber);
                command.Parameters.AddWithValue("@RoutingNumber", user.RoutingNumber);
                command.Parameters.AddWithValue("@AvailableBalance", user.AvailableBalance);
                command.Parameters.AddWithValue("@PaymentAmount", user.PaymentAmount);
                command.Parameters.AddWithValue("@VendorName", user.VendorName);
                command.Parameters.AddWithValue("@PaymentApprovalRequired", user.PaymentApprovalRequired);
                var newId = await command.ExecuteScalarAsync();
                user.Id = Convert.ToInt32(newId);
                return user;
            }
        }
        
        public async Task<bool> Update(UserModel user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password, PhoneNumber = @PhoneNumber, " +
                            "Income = @Income, Age = @Age, CreditScore = @CreditScore, BankAccountNumber = @BankAccountNumber, " +
                            "RoutingNumber = @RoutingNumber, AvailableBalance = @AvailableBalance, PaymentAmount = @PaymentAmount, " +
                            "VendorName = @VendorName, PaymentApprovalRequired = @PaymentApprovalRequired WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@Income", user.Income);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@CreditScore", user.CreditScore);
                command.Parameters.AddWithValue("@BankAccountNumber", user.BankAccountNumber);
                command.Parameters.AddWithValue("@RoutingNumber", user.RoutingNumber);
                command.Parameters.AddWithValue("@AvailableBalance", user.AvailableBalance);
                command.Parameters.AddWithValue("@PaymentAmount", user.PaymentAmount);
                command.Parameters.AddWithValue("@VendorName", user.VendorName);
                command.Parameters.AddWithValue("@PaymentApprovalRequired", user.PaymentApprovalRequired);
                command.Parameters.AddWithValue("@Id", user.Id);
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
        
        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Users WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
        
        private UserModel MapUserModelFromReader(SqlDataReader reader)
        {
            return new UserModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Email = reader["Email"].ToString(),
                Password = reader["Password"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Income = Convert.ToDouble(reader["Income"]),
                Age = Convert.ToInt32(reader["Age"]),
                CreditScore = Convert.ToInt32(reader["CreditScore"]),
                BankAccountNumber = reader["BankAccountNumber"].ToString(),
                RoutingNumber = reader["RoutingNumber"].ToString(),
                AvailableBalance = Convert.ToDouble(reader["AvailableBalance"]),
                PaymentAmount = Convert.ToDouble(reader["PaymentAmount"]),
                VendorName = reader["VendorName"].ToString(),
                PaymentApprovalRequired = Convert.ToBoolean(reader["PaymentApprovalRequired"])
            };
        }
    }
}