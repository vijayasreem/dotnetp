using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class GreetingRepository : IGreetingRepository
    {
        private readonly string _connectionString;

        public GreetingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GreetingModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT Id, Message FROM GreetingTable WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new GreetingModel
                        {
                            Id = reader.GetInt32(0),
                            Message = reader.GetString(1)
                        };
                    }
                }
            }

            return null;
        }

        public async Task CreateAsync(GreetingModel greeting)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("INSERT INTO GreetingTable (Message) VALUES (@Message)", connection);
                command.Parameters.AddWithValue("@Message", greeting.Message);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(GreetingModel greeting)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("UPDATE GreetingTable SET Message = @Message WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", greeting.Id);
                command.Parameters.AddWithValue("@Message", greeting.Message);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("DELETE FROM GreetingTable WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}