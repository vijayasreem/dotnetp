using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace dotnetp
{
    public class UserStoryRepository : IUserStoryRepository
    {
        private string connectionString = "your_connection_string_here";

        public async Task<UserStoryModel> GetByIdAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM UserStories WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return MapUserStory(reader);
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<UserStoryModel>> GetAllAsync()
        {
            List<UserStoryModel> userStories = new List<UserStoryModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM UserStories", connection);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        userStories.Add(MapUserStory(reader));
                    }
                }
            }

            return userStories;
        }

        public async Task AddAsync(UserStoryModel userStory)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO UserStories (Id, ProductCode, ProductDescription, EffectiveStartDate, EffectiveEndDate, BusinessType, MinTerm, MaxTerm, NumberOfAdults, NumberOfChildren, MinAgeAllowed, MaxAgeAllowed, Relationship, Occupation, RatingCalculator) " +
                                                    "VALUES (@Id, @ProductCode, @ProductDescription, @EffectiveStartDate, @EffectiveEndDate, @BusinessType, @MinTerm, @MaxTerm, @NumberOfAdults, @NumberOfChildren, @MinAgeAllowed, @MaxAgeAllowed, @Relationship, @Occupation, @RatingCalculator)", connection);
                command.Parameters.AddWithValue("@Id", userStory.Id);
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

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(UserStoryModel userStory)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE UserStories SET ProductCode = @ProductCode, ProductDescription = @ProductDescription, EffectiveStartDate = @EffectiveStartDate, EffectiveEndDate = @EffectiveEndDate, BusinessType = @BusinessType, MinTerm = @MinTerm, MaxTerm = @MaxTerm, NumberOfAdults = @NumberOfAdults, NumberOfChildren = @NumberOfChildren, MinAgeAllowed = @MinAgeAllowed, MaxAgeAllowed = @MaxAgeAllowed, Relationship = @Relationship, Occupation = @Occupation, RatingCalculator = @RatingCalculator " +
                                                    "WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", userStory.Id);
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

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DELETE FROM UserStories WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        private UserStoryModel MapUserStory(SqlDataReader reader)
        {
            return new UserStoryModel
            {
                Id = reader.GetString(reader.GetOrdinal("Id")),
                ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                ProductDescription = reader.GetString(reader.GetOrdinal("ProductDescription")),
                EffectiveStartDate = reader.GetDateTime(reader.GetOrdinal("EffectiveStartDate")),
                EffectiveEndDate = reader.GetDateTime(reader.GetOrdinal("EffectiveEndDate")),
                BusinessType = reader.GetString(reader.GetOrdinal("BusinessType")),
                MinTerm = reader.GetInt32(reader.GetOrdinal("MinTerm")),
                MaxTerm = reader.GetInt32(reader.GetOrdinal("MaxTerm")),
                NumberOfAdults = reader.GetInt32(reader.GetOrdinal("NumberOfAdults")),
                NumberOfChildren = reader.GetInt32(reader.GetOrdinal("NumberOfChildren")),
                MinAgeAllowed = reader.GetInt32(reader.GetOrdinal("MinAgeAllowed")),
                MaxAgeAllowed = reader.GetInt32(reader.GetOrdinal("MaxAgeAllowed")),
                Relationship = reader.GetString(reader.GetOrdinal("Relationship")),
                Occupation = reader.GetString(reader.GetOrdinal("Occupation")),
                RatingCalculator = reader.GetString(reader.GetOrdinal("RatingCalculator"))
            };
        }
    }
}