using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class UserStoryRepository : IUserStoryService
    {
        private readonly string _connectionString;

        public UserStoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(UserStoryModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO UserStory (Description, AcceptanceCriteria) VALUES (@Description, @AcceptanceCriteria); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Description", model.Description);
                command.Parameters.AddWithValue("@AcceptanceCriteria", model.AcceptanceCriteria);

                int id = Convert.ToInt32(await command.ExecuteScalarAsync());

                return id;
            }
        }

        public async Task<UserStoryModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT Id, Description, AcceptanceCriteria FROM UserStory WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    UserStoryModel model = new UserStoryModel
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        AcceptanceCriteria = reader.GetString(2)
                    };

                    return model;
                }

                return null;
            }
        }

        public async Task<List<UserStoryModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT Id, Description, AcceptanceCriteria FROM UserStory", connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<UserStoryModel> models = new List<UserStoryModel>();

                while (reader.Read())
                {
                    UserStoryModel model = new UserStoryModel
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        AcceptanceCriteria = reader.GetString(2)
                    };

                    models.Add(model);
                }

                return models;
            }
        }

        public async Task UpdateAsync(UserStoryModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE UserStory SET Description = @Description, AcceptanceCriteria = @AcceptanceCriteria WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Description", model.Description);
                command.Parameters.AddWithValue("@AcceptanceCriteria", model.AcceptanceCriteria);
                command.Parameters.AddWithValue("@Id", model.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DELETE FROM UserStory WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}