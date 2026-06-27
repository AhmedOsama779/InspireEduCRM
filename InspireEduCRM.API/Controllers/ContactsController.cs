using InspireEduCRM.Application.DTOs.Contacts;
using InspireEduCRM.Application.Services.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireEduCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService) 
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactRequest request)
        {
            var contact = await _contactService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = contact.Id } ,contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , CreateContactRequest request)
        {
            var updated = await _contactService.UpdateAsync(id, request);
            if(updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpGet("by-school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId)
        {
            var contacts = await _contactService.GetBySchoolIdAsync(schoolId);
            return Ok(contacts);
        }
    }
}
