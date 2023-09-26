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

        public async Task CreateAsync(BookingModel booking)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("INSERT INTO Bookings (CheckInDate, IsCancelled) VALUES (@CheckInDate, @IsCancelled)", connection);
                command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT Id, CheckInDate, IsCancelled FROM Bookings WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (await reader.ReadAsync())
                    {
                        return new BookingModel
                        {
                            Id = reader.GetInt32(0),
                            CheckInDate = reader.GetDateTime(1),
                            IsCancelled = reader.GetBoolean(2)
                        };
                    }
                }
            }

            return null;
        }

        public async Task UpdateAsync(BookingModel booking)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("UPDATE Bookings SET CheckInDate = @CheckInDate, IsCancelled = @IsCancelled WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);
                command.Parameters.AddWithValue("@Id", booking.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("DELETE FROM Bookings WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}