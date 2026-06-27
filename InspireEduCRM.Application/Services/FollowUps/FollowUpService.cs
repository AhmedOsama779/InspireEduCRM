using InspireEduCRM.Application.DTOs.FollowUps;
using InspireEduCRM.Application.Services.Common;
using InspireEduCRM.Domain.Entities;
using InspireEduCRM.Domain.Enums;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireEduCRM.Application.Services.FollowUps
{
    public class FollowUpService : IFollowUpService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public FollowUpService(ApplicationDbContext context , ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<FollowUpDto> CreateAsync(CreateFollowUpRequest request)
        {
            var customerServiceRepId = _currentUserService.GetUserId();
            if (!Enum.TryParse<FollowUpType>(request.FollowUpType, true, out var followUpType))
                throw new ArgumentException($"Invalid follow-up type: {request.FollowUpType}");

            var followUp = new FollowUp
            {
                LeadId = request.LeadId,
                CustomerServiceRepId = customerServiceRepId,
                ContactId = request.ContactId,
                FollowUpDate = request.FollowUpDate,
                FollowUpType = followUpType,
                Summary = request.Summary,
                NextAction = request.NextAction,

            };
            _context.Add(followUp);
            // Business rule: logging a follow-up means the lead is actively progressing.
            // If it's still sitting at the very first "Lead" stage, bump it to "FollowUp"
            // stage automatically. We don't override later stages (e.g. don't push a
            // "Won" lead backwards just because a follow-up note was added).
            var lead = await _context.Leads.FindAsync(request.LeadId);
            if (lead != null && lead.Stage == LeadStage.Lead)
            {
                lead.Stage = LeadStage.FollowUp;
                lead.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return MapToDto(followUp);
        }

        public async Task<FollowUpDto?> GetByIdAsync(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);
            return followUp == null ? null : MapToDto(followUp);
        }

        public async Task<List<FollowUpDto>> GetByLeadIdAsync(int leadId)
        {
            return await _context.FollowUps
                .Where(f => f.LeadId == leadId)
                .Select(f=> new FollowUpDto
                {
                    Id = f.Id,
                    LeadId = f.LeadId,
                    CustomerServiceRepId = f.CustomerServiceRepId,
                    ContactId = f.ContactId,
                    FollowUpDate = f.FollowUpDate,
                    FollowUpType = f.FollowUpType.ToString(),
                    Summary = f.Summary,
                    NextAction = f.NextAction
                }).ToListAsync();
        }

        private static FollowUpDto MapToDto(FollowUp followUp)
        {
            return new FollowUpDto
            {
                Id = followUp.Id,
                LeadId = followUp.LeadId,
                CustomerServiceRepId = followUp.CustomerServiceRepId,
                ContactId = followUp.ContactId,
                FollowUpDate = followUp.FollowUpDate,
                FollowUpType = followUp.FollowUpType.ToString(),
                Summary = followUp.Summary,
                NextAction = followUp.NextAction,
            };
        }
    }
}
