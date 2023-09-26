
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class BookingModelRepository
    {
        private readonly string _connectionString;
        
        public BookingModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<int> CreateAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO Bookings (CheckInDate, CheckOutDate, CustomerName, CustomerEmail, IsCancelled) VALUES (@CheckInDate, @CheckOutDate, @CustomerName, @CustomerEmail, @IsCancelled); SELECT SCOPE_IDENTITY();";

                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", booking.CheckOutDate);
                    command.Parameters.AddWithValue("@CustomerName", booking.CustomerName);
                    command.Parameters.AddWithValue("@CustomerEmail", booking.CustomerEmail);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);

                    await connection.OpenAsync();
                    int newId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    
                    return newId;
                }
            }
        }
        
        public async Task<BookingModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Bookings WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapBookingModel(reader);
                        }
                    }
                }
            }
            
            return null;
        }
        
        public async Task<List<BookingModel>> GetAllAsync()
        {
            List<BookingModel> bookings = new List<BookingModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Bookings;";

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bookings.Add(MapBookingModel(reader));
                        }
                    }
                }
            }
            
            return bookings;
        }
        
        public async Task UpdateAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Bookings SET CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, CustomerName = @CustomerName, CustomerEmail = @CustomerEmail, IsCancelled = @IsCancelled WHERE Id = @Id;";

                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@CheckOutDate", booking.CheckOutDate);
                    command.Parameters.AddWithValue("@CustomerName", booking.CustomerName);
                    command.Parameters.AddWithValue("@CustomerEmail", booking.CustomerEmail);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);
                    command.Parameters.AddWithValue("@Id", booking.Id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM Bookings WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        private BookingModel MapBookingModel(SqlDataReader reader)
        {
            return new BookingModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                CustomerName = Convert.ToString(reader["CustomerName"]),
                CustomerEmail = Convert.ToString(reader["CustomerEmail"]),
                IsCancelled = Convert.ToBoolean(reader["IsCancelled"])
            };
        }
    }
}
