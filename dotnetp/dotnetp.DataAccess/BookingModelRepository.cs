using System;
using System.Collections.Generic;
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

                string query = "INSERT INTO Bookings (IsCancelled, CheckInDate, CancelationDeadline) " +
                               "VALUES (@IsCancelled, @CheckInDate, @CancelationDeadline);" +
                               "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);
                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@CancelationDeadline", booking.CancelationDeadline);

                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task<BookingModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Bookings WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

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

        public async Task<IEnumerable<BookingModel>> GetAllAsync()
        {
            List<BookingModel> bookings = new List<BookingModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Bookings;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public async Task<bool> UpdateAsync(BookingModel booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Bookings SET IsCancelled = @IsCancelled, CheckInDate = @CheckInDate, CancelationDeadline = @CancelationDeadline " +
                               "WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", booking.Id);
                    command.Parameters.AddWithValue("@IsCancelled", booking.IsCancelled);
                    command.Parameters.AddWithValue("@CheckInDate", booking.CheckInDate);
                    command.Parameters.AddWithValue("@CancelationDeadline", booking.CancelationDeadline);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Bookings WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }

        private BookingModel MapBookingModel(SqlDataReader reader)
        {
            return new BookingModel
            {
                Id = (int)reader["Id"],
                IsCancelled = (bool)reader["IsCancelled"],
                CheckInDate = (DateTime)reader["CheckInDate"],
                CancelationDeadline = (DateTime)reader["CancelationDeadline"]
            };
        }
    }
}