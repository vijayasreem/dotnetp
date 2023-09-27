using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class CancelBookingRepository : ICancelBookingService
    {
        private readonly string _connectionString;
        
        public CancelBookingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<CancelBookingModel> GetBookingByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                SqlCommand command = new SqlCommand("SELECT * FROM Bookings WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                
                SqlDataReader reader = await command.ExecuteReaderAsync();
                
                if (await reader.ReadAsync())
                {
                    return new CancelBookingModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CheckInDate = Convert.ToDateTime(reader["CheckInDate"])
                    };
                }
                
                return null;
            }
        }
        
        public async Task<int> CreateBookingAsync(CancelBookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                SqlCommand command = new SqlCommand("INSERT INTO Bookings (CheckInDate) VALUES (@CheckInDate); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }
        
        public async Task<bool> UpdateBookingAsync(CancelBookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                SqlCommand command = new SqlCommand("UPDATE Bookings SET CheckInDate = @CheckInDate WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                command.Parameters.AddWithValue("@Id", booking.Id);
                
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
        
        public async Task<bool> DeleteBookingAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                SqlCommand command = new SqlCommand("DELETE FROM Bookings WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
        
        public async Task<bool> CanCancelBookingAsync(int id)
        {
            CancelBookingModel booking = await GetBookingByIdAsync(id);
            
            if (booking != null)
            {
                DateTime currentDateTime = DateTime.Now;
                
                if (booking.CheckInDate.Subtract(currentDateTime).TotalHours > 24)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}