using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class EndorsementRepository : IEndorsementRepository
    {
        private readonly string _connectionString;

        public EndorsementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(EndorsementModel endorsement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Endorsements (Description, MakerActivity, CheckerActivity, Timestamp, UserId, RelevantDetails) VALUES (@Description, @MakerActivity, @CheckerActivity, @Timestamp, @UserId, @RelevantDetails); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Description", endorsement.Description);
                    command.Parameters.AddWithValue("@MakerActivity", endorsement.MakerActivity);
                    command.Parameters.AddWithValue("@CheckerActivity", endorsement.CheckerActivity);
                    command.Parameters.AddWithValue("@Timestamp", endorsement.Timestamp);
                    command.Parameters.AddWithValue("@UserId", endorsement.UserId);
                    command.Parameters.AddWithValue("@RelevantDetails", endorsement.RelevantDetails);

                    var insertedId = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(insertedId);
                }
            }
        }

        public async Task<EndorsementModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Endorsements WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapEndorsementFromReader(reader);
                        }

                        return null;
                    }
                }
            }
        }

        public async Task<IEnumerable<EndorsementModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Endorsements", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var endorsements = new List<EndorsementModel>();

                        while (await reader.ReadAsync())
                        {
                            endorsements.Add(MapEndorsementFromReader(reader));
                        }

                        return endorsements;
                    }
                }
            }
        }

        public async Task UpdateAsync(EndorsementModel endorsement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Endorsements SET Description = @Description, MakerActivity = @MakerActivity, CheckerActivity = @CheckerActivity, Timestamp = @Timestamp, UserId = @UserId, RelevantDetails = @RelevantDetails WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", endorsement.Id);
                    command.Parameters.AddWithValue("@Description", endorsement.Description);
                    command.Parameters.AddWithValue("@MakerActivity", endorsement.MakerActivity);
                    command.Parameters.AddWithValue("@CheckerActivity", endorsement.CheckerActivity);
                    command.Parameters.AddWithValue("@Timestamp", endorsement.Timestamp);
                    command.Parameters.AddWithValue("@UserId", endorsement.UserId);
                    command.Parameters.AddWithValue("@RelevantDetails", endorsement.RelevantDetails);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Endorsements WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private EndorsementModel MapEndorsementFromReader(SqlDataReader reader)
        {
            return new EndorsementModel
            {
                Id = (int)reader["Id"],
                Description = (string)reader["Description"],
                MakerActivity = (string)reader["MakerActivity"],
                CheckerActivity = (string)reader["CheckerActivity"],
                Timestamp = (DateTime)reader["Timestamp"],
                UserId = (string)reader["UserId"],
                RelevantDetails = (string)reader["RelevantDetails"]
            };
        }
    }
}