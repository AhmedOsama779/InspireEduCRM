using InspireEduCRM.Application.DTOs.Visits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Visits
{
    public interface IVisitService
    {
        Task<List<VisitDto>> GetBySchoolIdAsync(int schoolId);
        Task<VisitDto?> GetByIdAsync(int id);
        Task<VisitDto> CreateAsync(CreateVisitRequest request);
    }
}
