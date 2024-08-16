using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NerdySoftApi.Dto.AnnouncementDto;
using NerdySoftApi.Services.IServices;

namespace NerdySoftApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var announcement = await _announcementService.GetAllAsync();
        if (!announcement.Any())
            return Ok(new List<AnnouncementDto>());
        
        return Ok(announcement);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var announcement = await _announcementService.GetByIdAsync(id);
        if (announcement is null)
            return NotFound();
        
        return Ok(announcement);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAnnouncementDto createAnnouncementDto)
    {
        var createdAnnouncement = await _announcementService.CreateAsync(createAnnouncementDto);
        return CreatedAtAction(nameof(GetById), new { id = createdAnnouncement.Id },
            new { id = createdAnnouncement.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateAnnouncementDto updateAnnouncementDto)
    {
        var updated = await _announcementService.UpdateAsync(updateAnnouncementDto);
        if (!updated)
            return NotFound();
        return Ok(updated);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var announcement = await _announcementService.GetByIdAsync(id);
        if (announcement is null)
            return NotFound();

        var deleted = await _announcementService.DeleteAsync(id);
        if (!deleted)
            return BadRequest("Unable to delete the operation due to business rules or constraints.");

        return NoContent();
    }
}