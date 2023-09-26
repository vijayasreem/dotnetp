using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;

namespace dotnetp
{
    public class CurrencyConversionRepository : ICurrencyConversionRepository
    {
        private readonly string _connectionString;

        public CurrencyConversionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CurrencyConversionModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO CurrencyConversion (Currency, Amount) VALUES (@Currency, @Amount); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Currency", model.Currency);
                command.Parameters.AddWithValue("@Amount", model.Amount);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<CurrencyConversionModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM CurrencyConversion WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CurrencyConversionModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Currency = reader["Currency"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"])
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<CurrencyConversionModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM CurrencyConversion;", connection);

                List<CurrencyConversionModel> models = new List<CurrencyConversionModel>();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        models.Add(new CurrencyConversionModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Currency = reader["Currency"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"])
                        });
                    }
                }

                return models;
            }
        }

        public async Task UpdateAsync(CurrencyConversionModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE CurrencyConversion SET Currency = @Currency, Amount = @Amount WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Currency", model.Currency);
                command.Parameters.AddWithValue("@Amount", model.Amount);
                command.Parameters.AddWithValue("@Id", model.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("DELETE FROM CurrencyConversion WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}