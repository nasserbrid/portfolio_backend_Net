using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int contactId);

        Task<Contact> CreateContact(Contact contact);

        Task<Contact> UpdateContact(Contact contact);

        Task<bool> DeleteContactById(int contactId);
    }
}
