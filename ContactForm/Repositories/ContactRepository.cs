using ContactForm.Dtos;
using ContactForm.Models;
using ContactFormApi.Data;
using Microsoft.EntityFrameworkCore;
using ContactForm.Repositories.IRepositories;



namespace ContactFormApi.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return
                contact!;
        }

        public async Task<bool> UpdateContactAsync(int id, ContactDto contactDto)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            contact.Name = contactDto.Name;
            contact.Email = contactDto.Email;

            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> AddContactAsync(ContactDto contactDto)
        {
            var contact = new Contact
            {
                Name = contactDto.Name,
                Email = contactDto.Email,
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}