using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ContactManager.Entities;

namespace ContactManager.Repositories
{
    public interface IContactAddressRepository
    {
        Task<IEnumerable<ContactAddress>> GetAddressForContact(long contactId);
    }
    
    public class ContactAddressRepository : Repository, IContactAddressRepository
    {
        public ContactAddressRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<IEnumerable<ContactAddress>> GetAddressForContact(long contactId)
        {
            throw new System.NotImplementedException();
        }
    }
}