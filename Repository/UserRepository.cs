using System.Data;
using System.Threading.Tasks;
using ContactManager.Entities;

namespace ContactManager.Repositories
{
    public interface IUserRepository
    {
        Task<long> UpsertUser(User user);
    }
    
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<long> UpsertUser(User user)
        {
            const string query =
                @"INSERT INTO users.user(email, first_name, last_name) VALUES (@Email, @FirstName, @LastName)
                                   ON CONFLICT (email) DO
                                   UPDATE
                                   SET first_name = @FirstName, last_name = @LastName, modified_at = now()
                                   RETURNING id";

            return await QueryRowAsync<long>(query, user);
        }
    }
}