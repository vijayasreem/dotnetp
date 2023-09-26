using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace dotnetp
{
    public class CurrencyConversionModelRepository : ICurrencyConversionModelRepository
    {
        private readonly string _connectionString;

        public CurrencyConversionModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CurrencyConversionModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO CurrencyConversionModels (PriceInMYR, TargetCurrency) VALUES (@PriceInMYR, @TargetCurrency); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PriceInMYR", model.PriceInMYR);
                    command.Parameters.AddWithValue("@TargetCurrency", model.TargetCurrency);

                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task<CurrencyConversionModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT Id, PriceInMYR, TargetCurrency FROM CurrencyConversionModels WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new CurrencyConversionModel
                            {
                                Id = reader.GetInt32(0),
                                PriceInMYR = reader.GetDecimal(1),
                                TargetCurrency = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<CurrencyConversionModel>> GetAllAsync()
        {
            var models = new List<CurrencyConversionModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT Id, PriceInMYR, TargetCurrency FROM CurrencyConversionModels";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            models.Add(new CurrencyConversionModel
                            {
                                Id = reader.GetInt32(0),
                                PriceInMYR = reader.GetDecimal(1),
                                TargetCurrency = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return models;
        }

        public async Task<bool> UpdateAsync(CurrencyConversionModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE CurrencyConversionModels SET PriceInMYR = @PriceInMYR, TargetCurrency = @TargetCurrency WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PriceInMYR", model.PriceInMYR);
                    command.Parameters.AddWithValue("@TargetCurrency", model.TargetCurrency);
                    command.Parameters.AddWithValue("@Id", model.Id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "DELETE FROM CurrencyConversionModels WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}