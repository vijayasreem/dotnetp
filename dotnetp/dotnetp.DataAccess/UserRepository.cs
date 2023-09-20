using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Users WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapUserModel(reader);
                    }

                    return null;
                }
            }
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Users";
                var command = new SqlCommand(query, connection);

                var users = new List<UserModel>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(MapUserModel(reader));
                    }
                }

                return users;
            }
        }

        public async Task<int> CreateAsync(UserModel user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO Users (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, @Password); SELECT SCOPE_IDENTITY();";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                return (int)await command.ExecuteScalarAsync();
            }
        }

        public async Task<bool> UpdateAsync(UserModel user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password WHERE Id = @Id";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "DELETE FROM Users WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        private UserModel MapUserModel(SqlDataReader reader)
        {
            return new UserModel
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Email = (string)reader["Email"],
                Password = (string)reader["Password"]
            };
        }
    }
}