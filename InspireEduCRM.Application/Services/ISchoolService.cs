using InspireEduCRM.Application.DTOs.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services
{
    public interface ISchoolService
    {
        Task<List<SchoolDto>> GetAllAsync();
        Task<SchoolDto?> GetByIdAsync(int id);
        Task<SchoolDto> CreateAsync(CreateSchoolRequest request);
        Task<SchoolDto?> UpdateAsync(int id, CreateSchoolRequest request);
    }
}
