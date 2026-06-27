using InspireEduCRM.Application.DTOs.FollowUps;
using InspireEduCRM.Application.Services.FollowUps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireEduCRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FollowUpsController : ControllerBase
{
    private readonly IFollowUpService _followUpService;

    public FollowUpsController(IFollowUpService followUpService)
    {
        _followUpService = followUpService;
    }

    [HttpGet("by-lead/{leadId}")]
    public async Task<IActionResult> GetByLead(int leadId)
    {
        var followUps = await _followUpService.GetByLeadIdAsync(leadId);
        return Ok(followUps);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var followUp = await _followUpService.GetByIdAsync(id);
        if (followUp == null)
            return NotFound();

        return Ok(followUp);
    }

    [HttpPost]
    [Authorize(Roles = "CustomerService,Admin")]
    public async Task<IActionResult> Create(CreateFollowUpRequest request)
    {
        try
        {
            var created = await _followUpService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}