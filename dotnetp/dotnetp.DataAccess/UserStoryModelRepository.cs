using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace dotnetp
{
    public class UserStoryModelRepository : IUserStoryModelRepository
    {
        private string _connectionString;

        public UserStoryModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserStoryModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT * FROM UserStoryModel WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    return MapUserStoryModel(reader);
                }

                return null;
            }
        }

        public async Task<List<UserStoryModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "SELECT * FROM UserStoryModel";
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                List<UserStoryModel> userStoryModels = new List<UserStoryModel>();

                while (reader.Read())
                {
                    userStoryModels.Add(MapUserStoryModel(reader));
                }

                return userStoryModels;
            }
        }

        public async Task<int> AddAsync(UserStoryModel userStoryModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "INSERT INTO UserStoryModel (ProductCode, ProductDescription, EffectiveStartDate, EffectiveEndDate, BusinessType, MinTerm, MaxTerm, NumberOfAdults, NumberOfChildren, MinAgeAllowed, MaxAgeAllowed, Relationship, Occupation, RatingCalculator) VALUES (@ProductCode, @ProductDescription, @EffectiveStartDate, @EffectiveEndDate, @BusinessType, @MinTerm, @MaxTerm, @NumberOfAdults, @NumberOfChildren, @MinAgeAllowed, @MaxAgeAllowed, @Relationship, @Occupation, @RatingCalculator); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProductCode", userStoryModel.ProductCode);
                command.Parameters.AddWithValue("@ProductDescription", userStoryModel.ProductDescription);
                command.Parameters.AddWithValue("@EffectiveStartDate", userStoryModel.EffectiveStartDate);
                command.Parameters.AddWithValue("@EffectiveEndDate", userStoryModel.EffectiveEndDate);
                command.Parameters.AddWithValue("@BusinessType", userStoryModel.BusinessType);
                command.Parameters.AddWithValue("@MinTerm", userStoryModel.MinTerm);
                command.Parameters.AddWithValue("@MaxTerm", userStoryModel.MaxTerm);
                command.Parameters.AddWithValue("@NumberOfAdults", userStoryModel.NumberOfAdults);
                command.Parameters.AddWithValue("@NumberOfChildren", userStoryModel.NumberOfChildren);
                command.Parameters.AddWithValue("@MinAgeAllowed", userStoryModel.MinAgeAllowed);
                command.Parameters.AddWithValue("@MaxAgeAllowed", userStoryModel.MaxAgeAllowed);
                command.Parameters.AddWithValue("@Relationship", userStoryModel.Relationship);
                command.Parameters.AddWithValue("@Occupation", userStoryModel.Occupation);
                command.Parameters.AddWithValue("@RatingCalculator", userStoryModel.RatingCalculator);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<bool> UpdateAsync(UserStoryModel userStoryModel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "UPDATE UserStoryModel SET ProductCode = @ProductCode, ProductDescription = @ProductDescription, EffectiveStartDate = @EffectiveStartDate, EffectiveEndDate = @EffectiveEndDate, BusinessType = @BusinessType, MinTerm = @MinTerm, MaxTerm = @MaxTerm, NumberOfAdults = @NumberOfAdults, NumberOfChildren = @NumberOfChildren, MinAgeAllowed = @MinAgeAllowed, MaxAgeAllowed = @MaxAgeAllowed, Relationship = @Relationship, Occupation = @Occupation, RatingCalculator = @RatingCalculator WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProductCode", userStoryModel.ProductCode);
                command.Parameters.AddWithValue("@ProductDescription", userStoryModel.ProductDescription);
                command.Parameters.AddWithValue("@EffectiveStartDate", userStoryModel.EffectiveStartDate);
                command.Parameters.AddWithValue("@EffectiveEndDate", userStoryModel.EffectiveEndDate);
                command.Parameters.AddWithValue("@BusinessType", userStoryModel.BusinessType);
                command.Parameters.AddWithValue("@MinTerm", userStoryModel.MinTerm);
                command.Parameters.AddWithValue("@MaxTerm", userStoryModel.MaxTerm);
                command.Parameters.AddWithValue("@NumberOfAdults", userStoryModel.NumberOfAdults);
                command.Parameters.AddWithValue("@NumberOfChildren", userStoryModel.NumberOfChildren);
                command.Parameters.AddWithValue("@MinAgeAllowed", userStoryModel.MinAgeAllowed);
                command.Parameters.AddWithValue("@MaxAgeAllowed", userStoryModel.MaxAgeAllowed);
                command.Parameters.AddWithValue("@Relationship", userStoryModel.Relationship);
                command.Parameters.AddWithValue("@Occupation", userStoryModel.Occupation);
                command.Parameters.AddWithValue("@RatingCalculator", userStoryModel.RatingCalculator);
                command.Parameters.AddWithValue("@id", userStoryModel.Id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = "DELETE FROM UserStoryModel WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        private UserStoryModel MapUserStoryModel(SqlDataReader reader)
        {
            return new UserStoryModel
            {
                Id = (int)reader["Id"],
                ProductCode = reader["ProductCode"].ToString(),
                ProductDescription = reader["ProductDescription"].ToString(),
                EffectiveStartDate = (DateTime)reader["EffectiveStartDate"],
                EffectiveEndDate = (DateTime)reader["EffectiveEndDate"],
                BusinessType = reader["BusinessType"].ToString(),
                MinTerm = (int)reader["MinTerm"],
                MaxTerm = (int)reader["MaxTerm"],
                NumberOfAdults = (int)reader["NumberOfAdults"],
                NumberOfChildren = (int)reader["NumberOfChildren"],
                MinAgeAllowed = (int)reader["MinAgeAllowed"],
                MaxAgeAllowed = (int)reader["MaxAgeAllowed"],
                Relationship = reader["Relationship"].ToString(),
                Occupation = reader["Occupation"].ToString(),
                RatingCalculator = reader["RatingCalculator"].ToString(),
            };
        }
    }
}