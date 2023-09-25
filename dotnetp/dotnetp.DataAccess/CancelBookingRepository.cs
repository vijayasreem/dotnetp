using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class CancelBookingRepository : ICancelBookingService
    {
        private readonly string connectionString;

        public CancelBookingRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CancelBookingModel cancelBooking)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("INSERT INTO CancelBookings (CheckInDate, CustomerName, CustomerEmail) VALUES (@CheckInDate, @CustomerName, @CustomerEmail); SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@CheckInDate", cancelBooking.CheckInDate);
                command.Parameters.AddWithValue("@CustomerName", cancelBooking.CustomerName);
                command.Parameters.AddWithValue("@CustomerEmail", cancelBooking.CustomerEmail);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<CancelBookingModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT * FROM CancelBookings WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CancelBookingModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                            CustomerName = Convert.ToString(reader["CustomerName"]),
                            CustomerEmail = Convert.ToString(reader["CustomerEmail"])
                        };
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateAsync(CancelBookingModel cancelBooking)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("UPDATE CancelBookings SET CheckInDate = @CheckInDate, CustomerName = @CustomerName, CustomerEmail = @CustomerEmail WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@CheckInDate", cancelBooking.CheckInDate);
                command.Parameters.AddWithValue("@CustomerName", cancelBooking.CustomerName);
                command.Parameters.AddWithValue("@CustomerEmail", cancelBooking.CustomerEmail);
                command.Parameters.AddWithValue("@Id", cancelBooking.Id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("DELETE FROM CancelBookings WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}