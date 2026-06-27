using InspireEduCRM.Application.DTOs.Contacts;
using InspireEduCRM.Domain.Entities;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactDto>> GetAllAsync()
        {
            return await _context.Contacts.Select(c => MapToDto(c)).ToListAsync();
        }

        private static ContactDto MapToDto(Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                SchoolId = contact.SchoolId,
                Name = contact.Name,
                Email = contact.Email,
                Mobile = contact.Mobile,
                Position = contact.Position,

            };
        }

        public async Task<ContactDto?> GetByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact == null ? null : MapToDto(contact);
        }

        public async Task<ContactDto> CreateAsync(CreateContactRequest request)
        {
            var contact = new Contact
            {
                SchoolId= request.SchoolId,
                Name = request.Name,
                Email = request.Email,
                Mobile= request.Mobile,
                Position = request.Position,
            };
             _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return MapToDto(contact);

        }
        public async Task<List<ContactDto>> GetBySchoolIdAsync(int schoolId)
        {
            return await _context.Contacts
                .Where(c => c.SchoolId == schoolId)
                .Select(c => MapToDto(c))
                .ToListAsync();
        }

        public async Task<ContactDto?> UpdateAsync(int id, CreateContactRequest request)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact == null) return null;
            contact.SchoolId = request.SchoolId;
            contact.Name = request.Name;
            contact.Email = request.Email;
            contact.Mobile = request.Mobile;
            contact.Position = request.Position;

            await _context.SaveChangesAsync();
            return MapToDto(contact);
        }

    }
}
