using Dapper;
using System.Data.SqlClient;

namespace Carteira.Investimentos.Admin.Repositories
{
    public class BaseRepository
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> ExecuteAsync(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AdminConnection")))
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AdminConnection")))
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("AdminConnection")))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }
    }
}
