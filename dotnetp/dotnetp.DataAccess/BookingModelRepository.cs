using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class BookingModelRepository : IBookingModelRepository
    {
        private readonly string connectionString;

        public BookingModelRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> CreateBookingAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Bookings (CheckInDate, IsCancelled) VALUES (@CheckInDate, @IsCancelled); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);

                    await connection.OpenAsync();
                    int newId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    return newId;
                }
            }
        }

        public async Task<BookingModel> GetBookingByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Bookings WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new BookingModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                                IsCancelled = Convert.ToBoolean(reader["IsCancelled"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<bool> UpdateBookingAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Bookings SET CheckInDate = @CheckInDate, IsCancelled = @IsCancelled WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);
                    command.Parameters.AddWithValue("@Id", booking.Id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Bookings WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }
    }
}