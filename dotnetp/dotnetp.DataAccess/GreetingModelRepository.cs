using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class GreetingModelRepository : IGreetingModelRepository
    {
        private readonly string _connectionString;

        public GreetingModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<GreetingModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM GreetingModel WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapToGreetingModel(reader);
                    }
                }
            }

            return null;
        }

        public async Task<List<GreetingModel>> GetAllAsync()
        {
            List<GreetingModel> greetingModels = new List<GreetingModel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM GreetingModel", connection);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        greetingModels.Add(MapToGreetingModel(reader));
                    }
                }
            }

            return greetingModels;
        }

        public async Task<int> AddAsync(GreetingModel greetingModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO GreetingModel (WelcomingMessage) VALUES (@WelcomingMessage); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@WelcomingMessage", greetingModel.WelcomingMessage);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<bool> UpdateAsync(GreetingModel greetingModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE GreetingModel SET WelcomingMessage = @WelcomingMessage WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@WelcomingMessage", greetingModel.WelcomingMessage);
                command.Parameters.AddWithValue("@Id", greetingModel.Id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DELETE FROM GreetingModel WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        private GreetingModel MapToGreetingModel(SqlDataReader reader)
        {
            return new GreetingModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                WelcomingMessage = reader["WelcomingMessage"].ToString()
            };
        }
    }
}