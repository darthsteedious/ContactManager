using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SQLitePCL;

namespace ContactManager.Repositories
{
    public abstract class Repository
    {
        private readonly IDbConnection _connection;

        protected Repository(IDbConnection connection)
        {
            _connection = connection;

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string query, object param = null, IDbTransaction transaction = null)
        {
            using (_connection)
            {
                return await _connection.QueryAsync<T>(query, param, transaction);
            }
        }

        protected async Task<T> QueryRowAsync<T>(string query, object param = null, IDbTransaction transaction = null)
        {
            using (_connection)
            {
                return await _connection.QueryFirstAsync<T>(query, param, transaction);
            }
        }

        protected async Task<long> ExecuteAsync(string query, object param = null, IDbTransaction transaction = null)
        {
            using (_connection)
            {
                return await _connection.ExecuteAsync(query, param, transaction);
            }
        }
    }
}