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

            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.PhoneNumber = contactDto.PhoneNumber;
            contact.Email = contactDto.Email;
            contact.City = contactDto.City;
            contact.Address = contactDto.Address;   

            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> AddContactAsync(ContactDto contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                PhoneNumber = contactDto.PhoneNumber,
                Email = contactDto.Email,
                City = contactDto.City,
                Address = contactDto.Address
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