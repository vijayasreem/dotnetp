using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class BookingCancellationRepository : IBookingCancellationRepository
    {
        private readonly string _connectionString;

        public BookingCancellationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(BookingCancellationModel bookingCancellation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO BookingCancellation (CheckInDate, CancellationDate) VALUES (@CheckInDate, @CancellationDate); SELECT SCOPE_IDENTITY();";

                command.Parameters.AddWithValue("@CheckInDate", bookingCancellation.CheckInDate);
                command.Parameters.AddWithValue("@CancellationDate", bookingCancellation.CancellationDate);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<BookingCancellationModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Id, CheckInDate, CancellationDate FROM BookingCancellation WHERE Id = @Id;";

                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new BookingCancellationModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                            CancellationDate = Convert.ToDateTime(reader["CancellationDate"])
                        };
                    }
                }

                return null;
            }
        }

        public async Task UpdateAsync(BookingCancellationModel bookingCancellation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE BookingCancellation SET CheckInDate = @CheckInDate, CancellationDate = @CancellationDate WHERE Id = @Id;";

                command.Parameters.AddWithValue("@Id", bookingCancellation.Id);
                command.Parameters.AddWithValue("@CheckInDate", bookingCancellation.CheckInDate);
                command.Parameters.AddWithValue("@CancellationDate", bookingCancellation.CancellationDate);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM BookingCancellation WHERE Id = @Id;";

                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}