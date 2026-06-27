using InspireEduCRM.Application.DTOs.Schools;
using InspireEduCRM.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireEduCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // any logged-in user, any role, can use these endpoints
    public class SchoolsController : Controller
    {
        private readonly ISchoolService _schoolService;
        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _schoolService.GetAllAsync();
            return Ok(schools);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var school = await _schoolService.GetByIdAsync(id);
            if (school == null) return NotFound();

            return Ok(school);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSchoolRequest request)
        {
            var created = await _schoolService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id , CreateSchoolRequest request)
        {
            var updated = await _schoolService.UpdateAsync(id, request);
            if(updated == null) return NotFound();

            return Ok(updated);
        }

    }
}
