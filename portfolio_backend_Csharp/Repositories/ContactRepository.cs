using Microsoft.EntityFrameworkCore;
using portfolio_backend_Csharp.Data;
using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ProjectBackendContext _projectBackendContext;

        public ContactRepository(ProjectBackendContext projectBackendContext)
        {
            _projectBackendContext = projectBackendContext;
        }
        public async Task<Contact> CreateContact(Contact contact)
        {
            var result = await _projectBackendContext.Contacts.AddAsync(contact);
            await _projectBackendContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteContactById(int contactId)
        {
            var contact = await _projectBackendContext.Contacts
                 .FindAsync(contactId);

            if (contact == null)
            {
                return false;
            }

            _projectBackendContext.Contacts.Remove(contact);
            await _projectBackendContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _projectBackendContext.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            return await _projectBackendContext.Contacts.FindAsync(contactId);
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            var existContact = await _projectBackendContext.Contacts
                .FindAsync(contact.Id);

            if (existContact != null)
            {
                existContact.Name = contact.Name;
                existContact.Email = contact.Email;
                existContact.Message = contact.Message;
                
                await _projectBackendContext.SaveChangesAsync();

                return existContact;
            }

            return null;
        }
    }
}
