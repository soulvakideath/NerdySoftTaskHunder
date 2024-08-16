using Microsoft.AspNetCore.Mvc;
using NerdySoftApi.Dto.AnnouncementDto;
using NerdySoftApi.Services;
using NerdySoftApi.Services.IServices;

namespace NerdySoftApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;

    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindSimilar(Guid id)
    {
        var similarAnnouncements = await _recommendationService.FindSimilarAsync(id);

        if (!similarAnnouncements.Any())
            return NotFound("No similar announcements found.");

        return Ok(similarAnnouncements);
    }
}