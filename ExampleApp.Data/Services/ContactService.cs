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
        Task<List<Contact>> GetFilteredContactsAsync(string lastNameStartsWith);
        Task SaveContactAsync(Contact contact);
        Task<Contact> GetContactAsync(Guid contactId);
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

        public Task<Contact> GetContactAsync(Guid contactId)
        {
            return _liteDbAsyncService.LiteDatabaseAsync.GetCollection<Contact>().FindByIdAsync(contactId);
        }

        public Task<List<Contact>> GetFilteredContactsAsync(string lastNameStartsWith)
        {
            return _liteDbAsyncService.LiteDatabaseAsync.GetCollection<Contact>()
                .Query()
                .Where(c => c.LastName.StartsWith(lastNameStartsWith))
                .ToListAsync();
        }

        public Task SaveContactAsync(Contact contact)
        {
            return _liteDbAsyncService.LiteDatabaseAsync.GetCollection<Contact>().UpsertAsync(contact);
        }

    }
}
