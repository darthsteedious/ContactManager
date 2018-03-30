using System.Threading.Tasks;
using System.Collections.Generic;
using ContactManager.Entities;
using System.Data;
using Dapper;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ContactManager.Repositories
{
    public interface IContactRepository {
        Task<IEnumerable<Contact>> GetContacts(UserContext context);
        Task<Contact> GetContactById(UserContext context, long contactId);
        Task<long> UpsertContact(UserContext context, Contact contact);
    }

    public class ContactRepository : Repository, IContactRepository
    {
        public ContactRepository(IDbConnection connection) : base(connection)
        {}

        public async Task<Contact> GetContactById(UserContext context, long contactId)
        {
            const string query = @"SELECT id, first_name, last_name
                                   FROM contact.contact
                                   WHERE id = @Id and owner = @Owner";
            
            var contact = await QueryRowAsync<Contact>(query, new {Id = contactId, Owner = context.Id});

            return contact;
        }

        public async Task<IEnumerable<Contact>> GetContacts(UserContext context)
        {
            const string query = @"SELECT id, first_name, last_name
                                   FROM contact.contact
                                   WHERE owner = @Id";

            
            var contacts = await QueryAsync<Contact>(query, new {context.Id});
            return contacts;
        }

        public async Task<long> UpsertContact(UserContext context, Contact contact) {
            const string text = @"INSERT INTO contact.contact(first_name, last_name) VALUES(@FirstName, @LastName) RETURNING id";

            if (contact.Id.HasValue)
            {
                await ExecuteAsync(@"UPDATE contact.contact
                                            SET first_name = @FirstName, last_name = @LastName, modified_at = now()
                                            WHERE id = @Id",
                    new {Id = contact.Id.Value, contact.FirstName, contact.LastName});

                return contact.Id.Value;

            }


            return await QueryRowAsync<long>(text, new {contact.FirstName, contact.LastName});
        }
    }
}
