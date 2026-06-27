using InspireEduCRM.Application.DTOs.Visits;
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

namespace InspireEduCRM.Application.Services.Visits
{
    public class VisitService : IVisitService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public VisitService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<VisitDto> CreateAsync(CreateVisitRequest request)
        {
            // 1. Get the current logged-in user's Id from the JWT token (never trust the client to say who they are)
            var salesRepId = _currentUserService.GetUserId();

            // 2. Parse the InterestLevel string into the actual enum
            if (!Enum.TryParse<InterestLevel>(request.InterestLevel, true, out var interestLevel))
                throw new ArgumentException($"Invalid interest level: {request.InterestLevel}");

            // 3. Create the Visit itself
            var visit = new Visit
            {
                SchoolId = request.SchoolID,
                ContactId = request.ContactId,
                SalesRepId = salesRepId,
                VisitDate = request.VisitDate,
                Notes = request.Notes,
                InterestLevel = interestLevel,
            };
            _context.Add(visit);
            await _context.SaveChangesAsync(); // Save now so visit.Id exists, needed for VisitBooks below

            // 4. Attach the Books that were presented (many-to-many join rows)
            foreach (var bookId in request.BookIds) 
            {
                _context.VisitBooks.Add(new VisitBook
                    {
                    VisitId = visit.Id,
                    BookId = bookId
                }
                );
            }

            // 5. Business rule: if this School doesn't have a Lead yet, create one now.
            // A Visit happening is what kicks off the sales pipeline.
            var existingLead = await _context.Leads.FirstOrDefaultAsync(l => l.SchoolId== request.SchoolID);
            if (existingLead == null) 
            {
                _context.Leads.Add(new Lead
                {
                    SchoolId= request.SchoolID,
                    Stage=LeadStage.Lead,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                });
            }
            await _context.SaveChangesAsync();
            return MapToDto(visit);

        }

        public async Task<VisitDto?> GetByIdAsync(int id)
        {
            var visit = await _context.Visits
                .Include(v => v.VisitBooks)
                .SingleOrDefaultAsync(v => v.Id==id);
            return visit == null ? null : MapToDto(visit);
        }

        public async Task<List<VisitDto>> GetBySchoolIdAsync(int schoolId)
        {
            return await _context.Visits
                .Where(v => v.SchoolId==schoolId)
                .Include(v => v.VisitBooks)
                .Select(v => MapToDto(v))
                .ToListAsync();
        }

        private static VisitDto MapToDto(Visit visit) 
        {
            return new VisitDto
            {
                Id = visit.Id,
                SchoolID = visit.SchoolId,
                ContactId = visit.ContactId,
                SalesRepId = visit.SalesRepId,
                VisitDate = visit.VisitDate,
                Notes = visit.Notes,
                InterestLevel = visit.InterestLevel.ToString(),
                BookIds= visit.VisitBooks?.Select(vb=>vb.BookId).ToList() ?? new List<int>()

            };
        }
    }
}
