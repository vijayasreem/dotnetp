using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private readonly string _connectionString;

        public UserStoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserStoryModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM UserStories WHERE Id = @Id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapUserStory(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<UserStoryModel>> GetAllAsync()
        {
            var userStories = new List<UserStoryModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM UserStories";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            userStories.Add(MapUserStory(reader));
                        }
                    }
                }
            }
            return userStories;
        }

        public async Task<int> CreateAsync(UserStoryModel userStory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO UserStories (ProductCode, ProductDescription, EffectiveStartDate, EffectiveEndDate, BusinessType, MinTerm, MaxTerm, NumberOfAdults, NumberOfChildren, MinAgeAllowed, MaxAgeAllowed, Relationship, Occupation, RatingCalculator) " +
                          "VALUES (@ProductCode, @ProductDescription, @EffectiveStartDate, @EffectiveEndDate, @BusinessType, @MinTerm, @MaxTerm, @NumberOfAdults, @NumberOfChildren, @MinAgeAllowed, @MaxAgeAllowed, @Relationship, @Occupation, @RatingCalculator);" +
                          "SELECT SCOPE_IDENTITY();";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductCode", userStory.ProductCode);
                    command.Parameters.AddWithValue("@ProductDescription", userStory.ProductDescription);
                    command.Parameters.AddWithValue("@EffectiveStartDate", userStory.EffectiveStartDate);
                    command.Parameters.AddWithValue("@EffectiveEndDate", userStory.EffectiveEndDate);
                    command.Parameters.AddWithValue("@BusinessType", userStory.BusinessType);
                    command.Parameters.AddWithValue("@MinTerm", userStory.MinTerm);
                    command.Parameters.AddWithValue("@MaxTerm", userStory.MaxTerm);
                    command.Parameters.AddWithValue("@NumberOfAdults", userStory.NumberOfAdults);
                    command.Parameters.AddWithValue("@NumberOfChildren", userStory.NumberOfChildren);
                    command.Parameters.AddWithValue("@MinAgeAllowed", userStory.MinAgeAllowed);
                    command.Parameters.AddWithValue("@MaxAgeAllowed", userStory.MaxAgeAllowed);
                    command.Parameters.AddWithValue("@Relationship", userStory.Relationship);
                    command.Parameters.AddWithValue("@Occupation", userStory.Occupation);
                    command.Parameters.AddWithValue("@RatingCalculator", userStory.RatingCalculator);
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task UpdateAsync(UserStoryModel userStory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "UPDATE UserStories SET ProductCode = @ProductCode, ProductDescription = @ProductDescription, EffectiveStartDate = @EffectiveStartDate, " +
                          "EffectiveEndDate = @EffectiveEndDate, BusinessType = @BusinessType, MinTerm = @MinTerm, MaxTerm = @MaxTerm, NumberOfAdults = @NumberOfAdults, " +
                          "NumberOfChildren = @NumberOfChildren, MinAgeAllowed = @MinAgeAllowed, MaxAgeAllowed = @MaxAgeAllowed, Relationship = @Relationship, " +
                          "Occupation = @Occupation, RatingCalculator = @RatingCalculator WHERE Id = @Id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductCode", userStory.ProductCode);
                    command.Parameters.AddWithValue("@ProductDescription", userStory.ProductDescription);
                    command.Parameters.AddWithValue("@EffectiveStartDate", userStory.EffectiveStartDate);
                    command.Parameters.AddWithValue("@EffectiveEndDate", userStory.EffectiveEndDate);
                    command.Parameters.AddWithValue("@BusinessType", userStory.BusinessType);
                    command.Parameters.AddWithValue("@MinTerm", userStory.MinTerm);
                    command.Parameters.AddWithValue("@MaxTerm", userStory.MaxTerm);
                    command.Parameters.AddWithValue("@NumberOfAdults", userStory.NumberOfAdults);
                    command.Parameters.AddWithValue("@NumberOfChildren", userStory.NumberOfChildren);
                    command.Parameters.AddWithValue("@MinAgeAllowed", userStory.MinAgeAllowed);
                    command.Parameters.AddWithValue("@MaxAgeAllowed", userStory.MaxAgeAllowed);
                    command.Parameters.AddWithValue("@Relationship", userStory.Relationship);
                    command.Parameters.AddWithValue("@Occupation", userStory.Occupation);
                    command.Parameters.AddWithValue("@RatingCalculator", userStory.RatingCalculator);
                    command.Parameters.AddWithValue("@Id", userStory.Id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "DELETE FROM UserStories WHERE Id = @Id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private UserStoryModel MapUserStory(SqlDataReader reader)
        {
            return new UserStoryModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                ProductCode = reader["ProductCode"].ToString(),
                ProductDescription = reader["ProductDescription"].ToString(),
                EffectiveStartDate = Convert.ToDateTime(reader["EffectiveStartDate"]),
                EffectiveEndDate = Convert.ToDateTime(reader["EffectiveEndDate"]),
                BusinessType = reader["BusinessType"].ToString(),
                MinTerm = Convert.ToInt32(reader["MinTerm"]),
                MaxTerm = Convert.ToInt32(reader["MaxTerm"]),
                NumberOfAdults = Convert.ToInt32(reader["NumberOfAdults"]),
                NumberOfChildren = Convert.ToInt32(reader["NumberOfChildren"]),
                MinAgeAllowed = Convert.ToInt32(reader["MinAgeAllowed"]),
                MaxAgeAllowed = Convert.ToInt32(reader["MaxAgeAllowed"]),
                Relationship = reader["Relationship"].ToString(),
                Occupation = reader["Occupation"].ToString(),
                RatingCalculator = reader["RatingCalculator"].ToString()
            };
        }
    }
}