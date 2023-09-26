using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class BookingCancellationRepository : IBookingCancellationRepository
    {
        private readonly string connectionString;

        public BookingCancellationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<BookingCancellationModel> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM BookingCancellation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapBookingCancellationModel(reader);
                        }
                    }
                }
            }

            return null;
        }

        public async Task Create(BookingCancellationModel bookingCancellation)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO BookingCancellation (CancelationDate, CustomerId, BookingId, BookingStatus, Email, Reason) " +
                                                    "VALUES (@CancelationDate, @CustomerId, @BookingId, @BookingStatus, @Email, @Reason)", connection))
                {
                    command.Parameters.AddWithValue("@CancelationDate", bookingCancellation.CancelationDate);
                    command.Parameters.AddWithValue("@CustomerId", bookingCancellation.CustomerId);
                    command.Parameters.AddWithValue("@BookingId", bookingCancellation.BookingId);
                    command.Parameters.AddWithValue("@BookingStatus", bookingCancellation.BookingStatus);
                    command.Parameters.AddWithValue("@Email", bookingCancellation.Email);
                    command.Parameters.AddWithValue("@Reason", bookingCancellation.Reason);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task Update(BookingCancellationModel bookingCancellation)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE BookingCancellation SET CancelationDate = @CancelationDate, " +
                                                    "CustomerId = @CustomerId, BookingId = @BookingId, " +
                                                    "BookingStatus = @BookingStatus, Email = @Email, Reason = @Reason " +
                                                    "WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", bookingCancellation.Id);
                    command.Parameters.AddWithValue("@CancelationDate", bookingCancellation.CancelationDate);
                    command.Parameters.AddWithValue("@CustomerId", bookingCancellation.CustomerId);
                    command.Parameters.AddWithValue("@BookingId", bookingCancellation.BookingId);
                    command.Parameters.AddWithValue("@BookingStatus", bookingCancellation.BookingStatus);
                    command.Parameters.AddWithValue("@Email", bookingCancellation.Email);
                    command.Parameters.AddWithValue("@Reason", bookingCancellation.Reason);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM BookingCancellation WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private BookingCancellationModel MapBookingCancellationModel(IDataReader reader)
        {
            return new BookingCancellationModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                CancelationDate = Convert.ToDateTime(reader["CancelationDate"]),
                CustomerId = Convert.ToString(reader["CustomerId"]),
                BookingId = Convert.ToString(reader["BookingId"]),
                BookingStatus = Convert.ToString(reader["BookingStatus"]),
                Email = Convert.ToString(reader["Email"]),
                Reason = Convert.ToString(reader["Reason"])
            };
        }
    }
}