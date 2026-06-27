using InspireEduCRM.Application.DTOs.Leads;
using InspireEduCRM.Application.Services.Leads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireEduCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LeadsController : Controller
    {
        private readonly ILeadService _leadService;
        public LeadsController(ILeadService leadService) 
        { 
            _leadService = leadService;    
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leads = await _leadService.GetAllAsync();
            return Ok(leads);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lead = await _leadService.GetByIdAsync(id);
            if (lead == null) return NotFound();
            return Ok(lead);
        }

        [HttpGet("by-school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId)
        {
            var lead = await _leadService.GetBySchoolIdAsync(schoolId);
            if (lead == null) return NotFound();
            return Ok(lead);
        }

        [HttpPut("{id}/Stage")]
        public async Task<IActionResult> UpdateStage(int id, UpdateLeadStageRequest request)
        {
            try
            {
                var updated = await _leadService.UpdateStageAsync(id, request);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
