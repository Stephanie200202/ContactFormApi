using ContactForm.Dtos;
using ContactForm.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactForm.Repositories.IRepositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<int> AddContactAsync(ContactDto contactDto);
        Task<bool> DeleteContactAsync(int id);
        Task<bool> UpdateContactAsync(int id, ContactDto contactDto);
       
        Task<Contact> GetContactByIdAsync(int id);
    }
}
