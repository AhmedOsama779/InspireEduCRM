using InspireEduCRM.Application.DTOs.FollowUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.FollowUps
{
    public interface IFollowUpService
    {
        Task<FollowUpDto?> GetByIdAsync(int id);
        Task<List<FollowUpDto>> GetByLeadIdAsync(int leadId);
        Task<FollowUpDto> CreateAsync(CreateFollowUpRequest request);

    }
}
