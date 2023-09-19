using System;
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

        public async Task<int> CreateAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Bookings (CheckInDate, IsCancelled) VALUES (@CheckInDate, @IsCancelled); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, CheckInDate, IsCancelled FROM Bookings WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new BookingModel
                            {
                                Id = (int)reader["Id"],
                                CheckInDate = (DateTime)reader["CheckInDate"],
                                IsCancelled = (bool)reader["IsCancelled"]
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task UpdateAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Bookings SET CheckInDate = @CheckInDate, IsCancelled = @IsCancelled WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);
                    command.Parameters.AddWithValue("@Id", booking.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Bookings WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}