using ExampleApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Data.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllContactsAsync();
        Task<IList<Contact>> GetFilteredContacts(string lastNameStartsWith);
        Task SaveContactAsync(Contact contact);
    }

    public class ContactService : IContactService
    {
        private readonly ILiteDbAsyncService _liteDbAsyncService;
        public ContactService(ILiteDbAsyncService liteDbAsyncService)
        {
            _liteDbAsyncService = liteDbAsyncService;
        }

        public Task<List<Contact>> GetAllContactsAsync()
        {
            return _liteDbAsyncService.LiteDatabaseAsync.GetCollection<Contact>().Query().ToListAsync();
        }

        public async Task<IList<Contact>> GetFilteredContacts(string lastNameStartsWith)
        {
            return new List<Contact>();
        }

        public Task SaveContactAsync(Contact contact)
        {
            return _liteDbAsyncService.LiteDatabaseAsync.GetCollection<Contact>().UpsertAsync(contact);
        }

    }
}
