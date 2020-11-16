using ExampleApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Data.Services
{
    public interface IContactService
    {
        Task<IList<Contact>> GetAllContacts();
        Task<IList<Contact>> GetFilteredContacts(string lastNameStartsWith);
        Task SaveContact(Contact contact);
    }

    public class ContactService : IContactService
    {
        private readonly ILiteDbAsyncService _liteDbAsyncService;
        public ContactService(ILiteDbAsyncService liteDbAsyncService)
        {
            _liteDbAsyncService = liteDbAsyncService;
        }

        public async Task<IList<Contact>> GetAllContacts()
        {
            return await _liteDbAsyncService.LiteDatabaseAsync.GetCollection<Contact>().Query().ToListAsync();
        }

        public async Task<IList<Contact>> GetFilteredContacts(string lastNameStartsWith)
        {
            return new List<Contact>();
        }

        public async Task SaveContact(Contact contact)
        {

        }
    }
}
