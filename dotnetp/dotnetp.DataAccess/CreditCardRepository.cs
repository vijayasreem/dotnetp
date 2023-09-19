using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using dotnetp.DTO;
using Dapper;

namespace dotnetp
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly string _connectionString;

        public CreditCardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CreditCardModel>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM CreditCards";
                return await connection.QueryAsync<CreditCardModel>(sql);
            }
        }

        public async Task<CreditCardModel> GetByIdAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM CreditCards WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<CreditCardModel>(sql, new { Id = id });
            }
        }

        public async Task<int> CreateAsync(CreditCardModel creditCard)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO CreditCards (CardNumber, CVV, EPayment) VALUES (@CardNumber, @CVV, @EPayment); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await connection.ExecuteScalarAsync<int>(sql, creditCard);
            }
        }

        public async Task<bool> UpdateAsync(CreditCardModel creditCard)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE CreditCards SET CardNumber = @CardNumber, CVV = @CVV, EPayment = @EPayment WHERE Id = @Id";
                int rowsAffected = await connection.ExecuteAsync(sql, creditCard);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM CreditCards WHERE Id = @Id";
                int rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
                return rowsAffected > 0;
            }
        }
    }
}