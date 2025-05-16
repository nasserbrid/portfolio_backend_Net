using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using portfolio_backend_Csharp.Models;
using portfolio_backend_Csharp.Repositories;

namespace portfolio_backend_Csharp.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IConfiguration _config;

        public ContactService(IContactRepository contactRepository, IConfiguration config)
        {
            _contactRepository = contactRepository;
            _config = config;
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            // Envoi de l'e-mail
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    _config["Email:Username"],
                    _config["Email:Password"]
                ),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Email:Username"]),
                Subject = "Nouveau message de contact",
                Body = $"Nom: {contact.Name}\nEmail: {contact.Email}\nMessage:\n{contact.Message}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add("nasserbrid@gmail.com");

            await smtpClient.SendMailAsync(mailMessage);

            // Enregistrement en base
            var savedContact = await _contactRepository.CreateContact(contact);
            return savedContact;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _contactRepository.GetAllContacts();
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            return await _contactRepository.GetContactById(contactId);
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            return await _contactRepository.UpdateContact(contact);
        }

        public async Task<bool> DeleteContactById(int contactId)
        {
            return await _contactRepository.DeleteContactById(contactId);
        }
    }
}
