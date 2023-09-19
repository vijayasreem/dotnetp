using dotnetp.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetp
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly string _connectionString;

        public CreditCardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(CreditCardModel creditCard)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("INSERT INTO CreditCards (CardNumber, CVV, EPayment) VALUES (@CardNumber, @CVV, @EPayment); SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@CardNumber", creditCard.CardNumber);
                command.Parameters.AddWithValue("@CVV", creditCard.CVV);
                command.Parameters.AddWithValue("@EPayment", creditCard.EPayment);

                var id = await command.ExecuteScalarAsync();

                return Convert.ToInt32(id);
            }
        }

        public async Task<CreditCardModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT * FROM CreditCards WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var creditCard = new CreditCardModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CardNumber = Convert.ToString(reader["CardNumber"]),
                            CVV = Convert.ToString(reader["CVV"]),
                            EPayment = Convert.ToString(reader["EPayment"])
                        };

                        return creditCard;
                    }

                    return null;
                }
            }
        }

        public async Task<List<CreditCardModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("SELECT * FROM CreditCards;", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var creditCards = new List<CreditCardModel>();

                    while (await reader.ReadAsync())
                    {
                        var creditCard = new CreditCardModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CardNumber = Convert.ToString(reader["CardNumber"]),
                            CVV = Convert.ToString(reader["CVV"]),
                            EPayment = Convert.ToString(reader["EPayment"])
                        };

                        creditCards.Add(creditCard);
                    }

                    return creditCards;
                }
            }
        }

        public async Task UpdateAsync(CreditCardModel creditCard)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("UPDATE CreditCards SET CardNumber = @CardNumber, CVV = @CVV, EPayment = @EPayment WHERE Id = @Id;", connection);

                command.Parameters.AddWithValue("@Id", creditCard.Id);
                command.Parameters.AddWithValue("@CardNumber", creditCard.CardNumber);
                command.Parameters.AddWithValue("@CVV", creditCard.CVV);
                command.Parameters.AddWithValue("@EPayment", creditCard.EPayment);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand("DELETE FROM CreditCards WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}