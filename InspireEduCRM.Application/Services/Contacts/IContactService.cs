using InspireEduCRM.Application.DTOs.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Contacts
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllAsync();
        Task<ContactDto?> GetByIdAsync(int id);
        Task<ContactDto> CreateAsync (CreateContactRequest request);
        Task<ContactDto?> UpdateAsync(int id, CreateContactRequest request);
        Task<List<ContactDto>> GetBySchoolIdAsync(int schoolId);
    }
}
