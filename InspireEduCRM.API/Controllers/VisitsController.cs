using InspireEduCRM.Application.DTOs.Visits;
using InspireEduCRM.Application.Services.Visits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireEduCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VisitsController : Controller
    {
        private readonly IVisitService _visitService;
        public VisitsController(IVisitService visitService) {
            _visitService = visitService; 
        }
        [HttpGet("by-school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId)
        {
            var visits = await _visitService.GetBySchoolIdAsync(schoolId);
            return Ok(visits);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var visit = await _visitService.GetByIdAsync(id);
            if(visit == null) return NotFound();
            return Ok(visit);
        }

        [HttpPost]
        [Authorize(Roles = "SalesRepresentative,Admin")]
        public async Task<IActionResult> Create(CreateVisitRequest request)
        {
            var created = await _visitService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


    }
}
