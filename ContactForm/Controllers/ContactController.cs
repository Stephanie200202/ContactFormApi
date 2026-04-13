using ContactForm.Dtos;
using ContactForm.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactFormApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactDto contactDto)
        {
            var id = await _contactRepository.AddContactAsync(contactDto);
            return Ok(new { Message = "Contact form submitted", Id = id });
        }

        [Authorize]
        [HttpGet("{id}")] // READ ONE
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpGet] // READ ALL
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return Ok(contacts);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { Message = "Access Denied: You are not authorized to delete contacts." });
            }

            var result = await _contactRepository.DeleteContactAsync(id);
            if (!result) return NotFound();

            return Ok(new { Message = "Contact Deleted Successfully" });
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] ContactDto updateDto)
        {
            var result = await _contactRepository.UpdateContactAsync(id, updateDto);

            if (!result)
            {
                return NotFound(new { Message = "Contact not found" });
            }

            return Ok(new { Message = "Contact updated successfully", Data = updateDto });
        }
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> PartialUpdate(int id, [FromBody] JsonPatchDocument<ContactDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null) return NotFound();

            return Ok(new { Message = "Contact partially updated", Data = contact });
        }
    }
}