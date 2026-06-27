using InspireEduCRM.Application.DTOs.Leads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Leads
{
    public interface ILeadService
    {
        Task<List<LeadDto>> GetAllAsync();
        Task<LeadDto?> GetByIdAsync(int id);
        Task<LeadDto?> GetBySchoolIdAsync( int schoolId);
        Task<LeadDto?> UpdateStageAsync( int id , UpdateLeadStageRequest request );
    }
}
