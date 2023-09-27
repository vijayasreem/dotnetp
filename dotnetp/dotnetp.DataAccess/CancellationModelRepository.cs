using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace dotnetp
{
    public class CancellationModelRepository : ICancellationModelRepository
    {
        private readonly string connectionString;

        public CancellationModelRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CancellationModel cancellation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO Cancellations (CancellationDate, BookingId, CustomerEmail, Reason) VALUES (@CancellationDate, @BookingId, @CustomerEmail, @Reason); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@CancellationDate", cancellation.CancellationDate);
                command.Parameters.AddWithValue("@BookingId", cancellation.BookingId);
                command.Parameters.AddWithValue("@CustomerEmail", cancellation.CustomerEmail);
                command.Parameters.AddWithValue("@Reason", cancellation.Reason);

                int newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                return newId;
            }
        }

        public async Task<CancellationModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM Cancellations WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CancellationModel
                        {
                            Id = (int)reader["Id"],
                            CancellationDate = (DateTime)reader["CancellationDate"],
                            BookingId = (int)reader["BookingId"],
                            CustomerEmail = (string)reader["CustomerEmail"],
                            Reason = (string)reader["Reason"]
                        };
                    }
                }
            }

            return null;
        }

        public async Task<List<CancellationModel>> GetAllAsync()
        {
            List<CancellationModel> cancellations = new List<CancellationModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM Cancellations;", connection);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cancellations.Add(new CancellationModel
                        {
                            Id = (int)reader["Id"],
                            CancellationDate = (DateTime)reader["CancellationDate"],
                            BookingId = (int)reader["BookingId"],
                            CustomerEmail = (string)reader["CustomerEmail"],
                            Reason = (string)reader["Reason"]
                        });
                    }
                }
            }

            return cancellations;
        }

        public async Task<bool> UpdateAsync(CancellationModel cancellation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE Cancellations SET CancellationDate = @CancellationDate, BookingId = @BookingId, CustomerEmail = @CustomerEmail, Reason = @Reason WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@CancellationDate", cancellation.CancellationDate);
                command.Parameters.AddWithValue("@BookingId", cancellation.BookingId);
                command.Parameters.AddWithValue("@CustomerEmail", cancellation.CustomerEmail);
                command.Parameters.AddWithValue("@Reason", cancellation.Reason);
                command.Parameters.AddWithValue("@Id", cancellation.Id);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DELETE FROM Cancellations WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}