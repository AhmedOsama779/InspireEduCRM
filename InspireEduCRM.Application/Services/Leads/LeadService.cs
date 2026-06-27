using InspireEduCRM.Application.DTOs.Leads;
using InspireEduCRM.Domain.Entities;
using InspireEduCRM.Domain.Enums;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.Leads
{
    public class LeadService : ILeadService
    {
        private readonly ApplicationDbContext _context;

        public LeadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeadDto>> GetAllAsync()
        {
            return await _context.Leads
                 .Include(l => l.School)
                 .Select(l => new LeadDto
                 {
                     Id = l.Id,
                     SchoolId = l.SchoolId,
                     SchoolName = l.School.Name,
                     Stage = l.Stage.ToString(),
                     CreatedAt = l.CreatedAt,
                     UpdatedAt = l.UpdatedAt,
                 })
                 .ToListAsync();
        }

        public async Task<LeadDto?> GetByIdAsync(int id)
        {
            var lead = await _context.Leads
                .Include(l => l.School)
                .FirstOrDefaultAsync(l => l.Id == id);
            return lead == null ? null : MapToDto(lead);
        }

        public async Task<LeadDto?> GetBySchoolIdAsync(int schoolId)
        {
            var lead = await _context.Leads
                .Include(l => l.School)
                .FirstOrDefaultAsync(l => l.SchoolId == schoolId);
            return lead == null ? null : MapToDto(lead);
        }

        public async Task<LeadDto?> UpdateStageAsync(int id, UpdateLeadStageRequest request)
        {
            if(! Enum.TryParse<LeadStage>(request.Stage, true ,out var newStage))
                throw new ArgumentException($"Invalid lead stage: {request.Stage}");


            var lead = await _context.Leads
            .Include(l => l.School)
            .FirstOrDefaultAsync(l => l.Id == id);

             if (lead == null) return null; 

             lead.Stage=newStage;
             lead.UpdatedAt= DateTime.Now;

            await _context.SaveChangesAsync();
            return MapToDto(lead);
                

        }

        private static LeadDto MapToDto(Lead lead)
        {
            return new LeadDto
            {
                Id = lead.Id,
                SchoolId = lead.SchoolId,
                SchoolName = lead.School.Name,
                Stage = lead.Stage.ToString(),
                CreatedAt = lead.CreatedAt,
                UpdatedAt = lead.UpdatedAt,
            };
            
        }
    }
}
