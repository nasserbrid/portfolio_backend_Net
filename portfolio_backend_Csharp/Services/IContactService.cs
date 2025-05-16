using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int contactId);

        Task<Contact> CreateContact(Contact contact);

        Task<Contact> UpdateContact(Contact contact);

        Task<bool> DeleteContactById(int contactId);
    }
}
